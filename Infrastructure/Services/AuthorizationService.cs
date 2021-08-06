using Application.Common.Interfaces.Services;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl = "8fafdd68-a090-496f-8c9a-3442cf30dae6";

        public AuthorizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Authorize()
        {            
            var responseString = await _httpClient.GetStringAsync(_remoteServiceBaseUrl);

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
