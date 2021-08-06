using FluentAssertions;
using Infrastructure.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTests.Services
{
    public class EmailMessageServiceTests
    {
        private readonly EmailMessageService _stu;
        private readonly Mock<IHttpClientFactory> _httpClientFactory;


        public EmailMessageServiceTests()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _httpClientFactory.Setup(e => e.CreateClient()).Returns(new HttpClient()
            {
                BaseAddress = new Uri("http://o4d9z.mocklab.io/")
            });

            _stu = new EmailMessageService(_httpClientFactory.Object);
        }

        [Fact()]
        public async Task Authorize_ValidResponse_ShouldReturnTrueAsync()
        {
            // Arrange & Act
            bool result = await _stu.SendMessage();

            // Assert
            result.Should().BeTrue();
        }
    }
}
