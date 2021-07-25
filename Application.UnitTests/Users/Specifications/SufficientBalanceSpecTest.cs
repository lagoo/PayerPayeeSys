using Application.Users.Specifications;
using Domain.Entities;
using FluentAssertions;
using Xunit;
namespace Application.UnitTests.Users.Specifications
{
    public class SufficientBalanceSpecTest
    {
        [Fact]
        public void GivenValidUser_ShouldReturnTrue()
        {
            // Arrange                                    
            var user = new User("Iago", "92426261803", "iagogs@gmail.com", "123", 100);

            // Act
            var result = new SufficientBalanceSpec(100).IsSatisfiedBy(user);

            // Assert            
            result.Should().BeTrue();
        }


        [Fact]
        public void GivenInvalidUser_ShouldReturnTrue()
        {
            // Arrange                                    
            var user = new User("Iago", "92426261803", "iagogs@gmail.com", "123", 0);

            // Act
            var result = new SufficientBalanceSpec(100).IsSatisfiedBy(user);

            // Assert            
            result.Should().BeFalse();
        }
    }
}
