using Application.Users.Queries.GetUserDetail;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace Application.UnitTests.Users.Queries.GetUserDetail
{
    public class GetUserDetailQueryValidatorTests
    {
        private readonly GetUserDetailQueryValidator _stu;

        public GetUserDetailQueryValidatorTests()
        {
            _stu = new GetUserDetailQueryValidator();
        }

        [Fact()]
        public void Validate_InvalidCommand_ShouldReturnFalseWithValidationErros()
        {
            // Arrange
            GetUserDetailQuery query = new(0);

            // Act
            ValidationResult result = _stu.Validate(query);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
        }

        [Fact()]
        public void Validate_ValidQuery_ShouldReturnTrueWithoutValidationErros()
        {
            // Arrange
            GetUserDetailQuery query = new(1);

            // Act
            ValidationResult result = _stu.Validate(query);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }
}
