using Application.Common.Exceptions;
using Application.UnitTests.Core;
using Application.Users.Commands.CreateUser;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Users.Commands.CreateUser
{
    public class CreateUserCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidRequest_ShouldReturnTheNewUserId()
        {
            // Arrange            
            var sut = new CreateUserCommand.Handler(_context, _currentUserMock.Object);

            // Act
            var result = await sut.Handle(new CreateUserCommand("Mariana", "75332667817", "mariana.nogueira@gmail.com", "123"), CancellationToken.None);

            // Assert
            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handle_GivenInvalidRequest_ShouldThrowValidationException()
        {
            // Arrange            
            var sut = new CreateUserCommand.Handler(_context, _currentUserMock.Object);

            // Act - dados ja inseridos no banco & Assert            
            await Assert.ThrowsAnyAsync<ValidationException>(() => sut.Handle(new CreateUserCommand("Iago", "92426261803", "iagogs@gmail.com", "123"), CancellationToken.None));
        }
    }
}
