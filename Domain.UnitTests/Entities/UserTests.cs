using Domain.UnitTests.Core.Fixtures;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Entities
{
    [Collection(nameof(UserCollection))]
    public class UserTests
    {
        private readonly UserFixture _userFixture;

        public UserTests(UserFixture userFixture)
        {
            _userFixture = userFixture;
        }

        [Fact]
        public void IsValid_ValidParamns_ShouldNotFillValidationResultAndReturnTrue()
        {
            // Arrange
            var entity = _userFixture.GenerateValidEntity("123");

            // Act
            bool result = entity.IsValid();

            // Assert
            result.Should().BeTrue();
            entity.ValidationErros.Should().BeEmpty();
            entity.Password.Should().NotBe("123");
        }

        [Fact]        
        public void IsValid_InvalidParamns_ShouldFillValidationResultAndReturnFalse()
        {
            // Arrange
            var entity = _userFixture.GenerateInvalidEntity();

            // Act
            bool result = entity.IsValid();

            // Assert
            result.Should().BeFalse();
            entity.ValidationErros.Count.Should().Be(_userFixture.ValidationErrosExpected.Count);
            entity.ValidationErros.Should().BeEquivalentTo(_userFixture.ValidationErrosExpected);
        }
    }
}
