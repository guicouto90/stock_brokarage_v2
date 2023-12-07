using Domain.Entities;
using Domain.Models;
using Domain.Validation;
using FluentAssertions;

namespace UnitTests.Entities
{
    public class WalletUnitTest
    {
        [Fact]
        public void BuyStockMethod_UpdateTotalInvested_UpdateCurrentBalance() 
        {
            // Arrange
            var walletExample = new Wallet();
            var stockExample = new Stock("ITAU", "ITUB4");

            // Act
            walletExample.BuyStock(stockExample, 100);

            // Assert
            Assert.True(walletExample.TotalInvested > 0);
            Assert.True(walletExample.CurrentBalance > 0);
        }

        [Fact]
        public void SellStockMethod_UpdateTotalInvested_UpdateCurrentBalance()
        {
            // Arrange
            var walletExample = new Wallet();
            var stockExample = new Stock("ITAU", "ITUB4");
            var stockExample2 = new Stock("PETROBRAS", "PETR4");

            // Act
            walletExample.BuyStock(stockExample, 100);
            walletExample.BuyStock(stockExample2, 100);
            walletExample.SellStock(stockExample, 100);
            walletExample.SellStock(stockExample2, 100);

            // Assert
            Assert.True(walletExample.TotalInvested == 0);
            Assert.True(walletExample.CurrentBalance == 0);
        }

        [Fact]
        public void SellStockMethod_ShouldThrown_DomainException_When_ThereIsNoStockToSell()
        {
            // Arrange
            var walletExample = new Wallet();
            var stockExample = new Stock("ITAU", "ITUB4");

            // Act
            var action = () => walletExample.SellStock(stockExample, 100);

            // Assert
             action.Should()
               .Throw<DomainExceptionValidation>()
                .WithMessage("You dont have this stock in your wallet");
        }

        [Fact]
        public void UpdateCurrentBalanceMethod_Should_UpdateCurrentBalanceCorrectly()
        {
            // Arrange
            var walletExample = new Wallet();
            var stockExample = new Stock("ITAU", "ITUB4");
            var stockExample2 = new Stock("PETROBRAS", "PETR4");

            // Act
            walletExample.BuyStock(stockExample, 100);
            walletExample.BuyStock(stockExample2, 100);
            double balance = walletExample.CurrentBalance;
            double totalInvested = walletExample.TotalInvested;
            stockExample.UpdatePrice();
            stockExample2.UpdatePrice();
            walletExample.UpdateCurrentBalance();


            // Assert
            Assert.NotEqual(balance, walletExample.CurrentBalance);
            Assert.Equal(totalInvested, walletExample.TotalInvested);
        }
    }
}
