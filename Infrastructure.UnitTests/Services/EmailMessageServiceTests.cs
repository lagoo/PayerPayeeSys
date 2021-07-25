using FluentAssertions;
using Infrastructure.Services;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.UnitTests.Services
{
    public class EmailMessageServiceTests
    {
        private readonly EmailMessageService _stu;
        private readonly Mock<HttpMessageHandler> _mock;

        public EmailMessageServiceTests()
        {
            _mock = new Mock<HttpMessageHandler>();

            _stu = new EmailMessageService(new HttpClient(_mock.Object)
            {
                BaseAddress = new Uri("http://test.com/")
            });
        }

        [Fact()]
        public async Task Authorize_ValidResponse_ShouldReturnTrueAsync()
        {
            // Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{ ""Message"": ""Success""}")
            };
            _mock.Protected()
                 .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                 .ReturnsAsync(response);


            // Act
            bool result = await _stu.SendMessage();

            // Assert
            result.Should().BeTrue();
        }

        [Fact()]
        public async Task Authorize_InvalidResponse_ShouldReturnTrueAsync()
        {
            // Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{ ""Message"": ""Failure""}")
            };
            _mock.Protected()
                 .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                 .ReturnsAsync(response);

            // Act
            bool result = await _stu.SendMessage();

            // Assert
            result.Should().BeFalse();
        }
    }
}
