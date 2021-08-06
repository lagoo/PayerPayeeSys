using FluentAssertions;
using Infrastructure.Services;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTests.Services
{
    public class AuthorizationServiceTests
    {
        private readonly AuthorizationService _stu;
        private readonly Mock<IHttpClientFactory> _httpClientFactory;

        public AuthorizationServiceTests()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _httpClientFactory.Setup(e => e.CreateClient()).Returns(new HttpClient()
            {
                BaseAddress = new Uri("https://run.mocky.io/v3/")
            });

            _stu = new AuthorizationService(_httpClientFactory.Object);
        }

        [Fact()]
        public async Task Authorize_ValidResponse_ShouldReturnTrueAsync()
        {
            // Arrange & Act
            bool result = await _stu.Authorize();

            // Assert
            result.Should().BeTrue();
        }        
    }
}
