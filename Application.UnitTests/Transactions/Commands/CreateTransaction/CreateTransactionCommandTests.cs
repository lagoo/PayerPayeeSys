using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Transactions.Commands.CreateTransaction;
using Application.UnitTests.Core;
using Domain.Events;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Transactions.Commands.CreateTransaction
{    
    public class CreateTransactionCommandTests : CommandBase
    {
        private readonly CreateTransactionCommand.Handler _sut;        

        public CreateTransactionCommandTests()
        {
            _sut = autoMocker.CreateInstance<CreateTransactionCommand.Handler>();            
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldReturnTransactionGuid()
        {
            // Arrange                                    
            autoMocker.GetMock<IAuthorizationService>().Setup(e => e.Authorize()).Returns(Task.FromResult(true));

            // Act
            var result = await _sut.Handle(new CreateTransactionCommand()
            {
                Amount = 100,
                Payer = 1,
                Payee = 2
            }, CancellationToken.None);

            // Assert            
            Assert.True(Guid.TryParse(result.ToString(), out Guid guidResult));
            autoMocker.GetMock<IAuthorizationService>().Verify(v => v.Authorize(), Times.Once);
            autoMocker.GetMock<IDomainEventService>().Verify(v => v.AddEvent(It.IsAny<TransactionCreatedEvent>(), It.IsAny<EventTime>()));
        }

        [Fact]
        public async Task Handle_GivenInvalidPayerPayee_ShouldThrowNotFoundException()
        {
            // Arrange                        
            var command = new CreateTransactionCommand()
            {
                Amount = 100,
                Payer = -1,
                Payee = -2
            };

            // Act & Assert            
            await Assert.ThrowsAnyAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GivenInvalidPayerWithoutBalance_ShouldThrowWarningException()
        {
            // Arrange            
            var command = new CreateTransactionCommand()
            {
                Amount = 100,
                Payer = 1,
                Payee = 2
            };

            // Act & Assert            
            var ex = await Assert.ThrowsAnyAsync<WarningException>(() => _sut.Handle(command, CancellationToken.None));
            ex.Message.Should().Be("Saldo insuficiente para realizar a transação e/ou usuário é um lojista!");
        }

        [Fact]
        public async Task Handle_GivenInvalidPayerIsShopkeeper_ShouldThrowWarningException()
        {
            // Arrange                        
            var command = new CreateTransactionCommand()
            {
                Amount = 100,
                Payer = 3,
                Payee = 1
            };

            // Act & Assert            
            var ex = await Assert.ThrowsAnyAsync<WarningException>(() => _sut.Handle(command, CancellationToken.None));
            ex.Message.Should().Be("Saldo insuficiente para realizar a transação e/ou usuário é um lojista!");
        }


        [Fact]
        public async Task Handle_AuthorizeServiceReturnFalse_ShouldThrowWarningException()
        {
            // Arrange
            autoMocker.GetMock<IAuthorizationService>().Setup(e => e.Authorize()).Returns(Task.FromResult(false));            

            var command = new CreateTransactionCommand()
            {
                Amount = 100,
                Payer = 2,
                Payee = 1
            };

            // Act & Assert            
            var ex = await Assert.ThrowsAnyAsync<WarningException>(() => _sut.Handle(command, CancellationToken.None));
            ex.Message.Should().Be("Transação não autorizada!");
        }
    }
}
