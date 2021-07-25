using Application.Common.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailMessageService : IMessageService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl = "notify";

        public EmailMessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendMessage()
        {
            var responseString = await _httpClient.GetStringAsync(_remoteServiceBaseUrl);

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
