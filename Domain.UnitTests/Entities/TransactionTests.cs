using Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace Domain.UnitTests.Entities
{
    public class TransactionTests
    {
        [Fact]
        public void Ctor_ValidParamns_ShouldFillPayerAndPayee()
        {
            // Arrange & Act 
            var transaction = new Transaction(new Wallet(100), new Wallet(100), 10);

            //Assert
            transaction.Payer.Should().NotBeNull();
            transaction.Payee.Should().NotBeNull();
        }

        [Fact]
        public void Ctor_InvalidParamns_ShouldThrowExecption()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Transaction(null, null, 10));
        }        
    }
}
