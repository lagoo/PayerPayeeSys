using Common.Enums;
using Domain.Entities;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Domain.UnitTests.Entities
{
    public class WalletTests
    {
        [Fact]
        public void Ctor_WithoutInitialAmount_ShouldNotFillTransactions()
        {
            // Arrange & Act
            var stu = new Wallet(0);

            // Assert
            stu.Transactions.Should().BeEmpty();            
        }

        [Fact]
        public void Ctor_WithInitialAmount_ShouldFillTransactions()
        {
            // Arrange & Act
            var stu = new Wallet(100);

            // Assert
            stu.Transactions.Should().NotBeEmpty();
            stu.Transactions.Should().HaveCount(1);
            stu.Transactions.First().Amount.Should().Be(100);            
        }


        [Fact]
        public void In_ValidParamns_ShouldFillTransactions()
        {
            // Arrange & Act
            var stu = new Wallet(0);

            var result = stu.In(100);

            // Assert
            stu.Transactions.Should().NotBeEmpty();
            stu.Transactions.Should().HaveCount(1);
            result.Amount.Should().Be(100);
            result.OperationType.Should().Be(CurrencyTypeOperationEnum.input);
        }

        [Fact]
        public void In_InvalidParamns_ShouldThrowExecption()
        {
            // Arrange 
            var stu = new Wallet(-10);

            // Act & Assert                        
            Assert.Throws<ArgumentOutOfRangeException>(() => stu.In(-100));
        }


        [Fact]
        public void Out_ValidParamns_ShouldFillTransactions()
        {
            // Arrange & Act
            var stu = new Wallet(0);

            var result = stu.Out(100);

            // Assert
            stu.Transactions.Should().NotBeEmpty();
            stu.Transactions.Should().HaveCount(1);
            result.Amount.Should().Be(-100);
            result.OperationType.Should().Be(CurrencyTypeOperationEnum.output);
        }

        [Fact]
        public void Out_InvalidParamns_ShouldThrowExecption()
        {
            // Arrange 
            var stu = new Wallet(0);

            // Act & Assert                        
            Assert.Throws<ArgumentOutOfRangeException>(() => stu.Out(-100));
        }
    }
}
