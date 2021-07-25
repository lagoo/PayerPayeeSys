using Application.Transactions.Commands.CreateTransaction;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace Application.UnitTests.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandValidatorTests
    {
        private readonly CreateTransactionCommandValidator _stu;

        public CreateTransactionCommandValidatorTests()
        {
            _stu = new CreateTransactionCommandValidator();
        }

        [Fact()]
        public void Validate_InvalidCommand_ShouldReturnFalseWithValidationErros()
        {
            // Arrange
            CreateTransactionCommand command = new()
            {
                Amount = 0,
                Payee = 0,
                Payer = 0 
            };

            // Act
            ValidationResult result = _stu.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(3);
        }

        [Fact()]
        public void Validate_ValidQuery_ShouldReturnTrueWithoutValidationErros()
        {
            // Arrange
            CreateTransactionCommand command = new()
            {
                Amount = 100,
                Payee = 1,
                Payer = 2
            };

            // Act
            ValidationResult result = _stu.Validate(command);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }
}
