using Application.Users.Commands.CreateUser;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace Application.UnitTests.Users.Commands.CreateUser
{
    public class CreateUserCommandValidatorTests
    {
        private readonly CreateUserCommandValidator _stu;

        public CreateUserCommandValidatorTests()
        {
            _stu = new CreateUserCommandValidator();
        }

        [Fact()]
        public void Validate_InvalidCommand_ShouldReturnFalseWithValidationErros()
        {
            // Arrange
            CreateUserCommand command = new()
            {
                Name = "",
                Document = "",
                Email = "",
                Password = ""
            };

            // Act
            ValidationResult result = _stu.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();            
            result.Errors.Should().HaveCount(6);
        }

        [Fact()]
        public void Validate_ValidQuery_ShouldReturnTrueWithoutValidationErros()
        {
            // Arrange
            CreateUserCommand command = new()
            {
                Name = "Iago",
                Document = "92426261803",
                Email = "iagogs@gmail.com",
                Password = "123"
            };

            // Act
            ValidationResult result = _stu.Validate(command);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }
}
