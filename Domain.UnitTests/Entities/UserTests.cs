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

        [Fact(DisplayName = "Não deve preencher propriedade ValidationErros e retornar true")]
        [Trait("Domain", "User")]
        public void IsValid_ValidValues_ShouldNotFillValidationResultAndReturnTrue()
        {
            // Arrange
            var entity = _userFixture.GenerateValidEntity();

            // Act
            bool result = entity.IsValid();

            // Assert
            result.Should().BeTrue();
            entity.ValidationErros.Should().BeEmpty();
        }

        [Fact(DisplayName = "Deve preencher propriedade ValidationErros e retornar false")]
        [Trait("Domain", "User")]
        public void IsValid_InvalidValues_ShouldFillValidationResultAndReturnFalse()
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
