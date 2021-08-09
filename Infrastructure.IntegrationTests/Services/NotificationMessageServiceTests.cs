using FluentAssertions;
using Infrastructure.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTests.Services
{
    public class NotificationMessageServiceTests
    {
        private readonly NotificationMessageService _stu;

        public NotificationMessageServiceTests()
        {
            _stu = new NotificationMessageService(new HttpClient()
            {
                BaseAddress = new Uri("http://o4d9z.mocklab.io/")
            });
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
