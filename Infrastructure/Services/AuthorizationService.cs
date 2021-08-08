using Application.Common.Interfaces.Services;
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
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl = "8fafdd68-a090-496f-8c9a-3442cf30dae6";

        private static readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy =
            Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
                  .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        private static readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> _circuitBreakerPolicy =
            Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
                  .CircuitBreakerAsync(2, TimeSpan.FromMinutes(1));

        private readonly AsyncPolicyWrap<HttpResponseMessage> _resilientPolicy =
            _circuitBreakerPolicy.WrapAsync(_retryPolicy);

        public AuthorizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Authorize()
        {
            if (_circuitBreakerPolicy.CircuitState == CircuitState.Open)
                throw new Exception("Servico de autorização de transação indisponivel!");

            var response = await _resilientPolicy.ExecuteAsync(() => _httpClient.GetAsync(_remoteServiceBaseUrl));

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AuthorizationResponse>(responseString);

            return result.Success;
        }
    }

    public class AuthorizationResponse
    {
        public string Message { get; set; }

        public bool Success => Message == "Autorizado";
    }

}
