using Application.Common.Interfaces;
using Application.TransactionNotifications.Commands.SendTransactionNotifications;
using Application.UnitTests.Core;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.TransactionNotifications.Commands.SendTransactionNotifications
{
    public class SendTransactionNotificationsCommandTests : CommandBase
    {

        private readonly SendTransactionNotificationsCommand.Handler _sut;

        public SendTransactionNotificationsCommandTests()
        {
            _sut = autoMocker.CreateInstance<SendTransactionNotificationsCommand.Handler>();
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldReturnTransactionGuid()
        {
            // Arrange                                    
            autoMocker.GetMock<IMessageService>().Setup(e => e.SendMessage()).Returns(Task.FromResult(true));

            // Act
            var result = await _sut.Handle(new SendTransactionNotificationsCommand(), CancellationToken.None);

            // Assert                        
            autoMocker.GetMock<IMessageService>().Verify(v => v.SendMessage(), Times.Once);
            _context.TransactionNotifications.Where(e => e.Sended).Any().Should().BeTrue();
        }
    }
}
