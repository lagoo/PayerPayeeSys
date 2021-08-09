using Application.Common.Interfaces;
using Newtonsoft.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Wrap;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class NotificationMessageService : IMessageService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl = "notify";

        private static readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy =
            Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
                  .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        private static readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> _circuitBreakerPolicy =
            Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
                  .CircuitBreakerAsync(2, TimeSpan.FromMinutes(1));

        private readonly AsyncPolicyWrap<HttpResponseMessage> _resilientPolicy =
            _circuitBreakerPolicy.WrapAsync(_retryPolicy);

        public NotificationMessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendMessage()
        {
            if (_circuitBreakerPolicy.CircuitState == CircuitState.Open)
                throw new Exception("Servico de envio de notificação da transação indisponivel!");

            var response = await _resilientPolicy.ExecuteAsync(() => _httpClient.GetAsync(_remoteServiceBaseUrl));

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<MessageResponse>(responseString);

            return result.Success;
        }
    }

    public class MessageResponse
    {
        public string Message { get; set; }

        public bool Success => Message == "Success";
    }
}
