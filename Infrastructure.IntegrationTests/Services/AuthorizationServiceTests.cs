using FluentAssertions;
using Infrastructure.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTests.Services
{
    public class AuthorizationServiceTests
    {
        private readonly AuthorizationService _stu;        

        public AuthorizationServiceTests()
        {            
            _stu = new AuthorizationService(new HttpClient()
            {
                BaseAddress = new Uri("https://run.mocky.io/v3/")
            });
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
