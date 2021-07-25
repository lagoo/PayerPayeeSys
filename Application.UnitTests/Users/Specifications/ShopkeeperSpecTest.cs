using Application.Users.Specifications;
using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Users.Specifications
{
    public class ShopkeeperSpecTest
    {

        [Fact]
        public void GivenValidUser_ShouldReturnTrue()
        {
            // Arrange                                    
            var user = new User("Loja", "64879030000100", "loja@gmail.com", "123", 100);

            // Act
            var result = new ShopkeeperSpec().IsSatisfiedBy(user);

            // Assert            
            result.Should().BeTrue();
        }


        [Fact]
        public void GivenInvalidUser_ShouldReturnTrue()
        {
            // Arrange                                    
            var user = new User("Iago", "92426261803", "iagogs@gmail.com", "123", 0);

            // Act
            var result = new ShopkeeperSpec().IsSatisfiedBy(user);

            // Assert            
            result.Should().BeFalse();
        }
    }
}
