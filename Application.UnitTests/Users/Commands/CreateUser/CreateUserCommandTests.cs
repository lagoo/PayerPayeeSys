﻿using Application.Common.Exceptions;
using Application.UnitTests.Core;
using Application.Users.Commands.CreateUser;
using FluentAssertions;
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
            var result = await sut.Handle(new CreateUserCommand()
            {
                Name = "Mariana",
                Document = "75332667817",
                Email = "mariana.nogueira@gmail.com",
                Password = "123"
            }, CancellationToken.None);

            // Assert
            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handle_GivenInvalidRequest_ShouldThrowValidationException()
        {
            // Arrange            
            var sut = new CreateUserCommand.Handler(_context, _currentUserMock.Object);

            // Act - dados ja inseridos no banco & Assert            
            await Assert.ThrowsAnyAsync<ValidationException>(() => sut.Handle(new CreateUserCommand() { Name= "Iago", Document = "92426261803", Email = "iagogs@gmail.com", Password = "123" }, CancellationToken.None));
        }
    }
}
