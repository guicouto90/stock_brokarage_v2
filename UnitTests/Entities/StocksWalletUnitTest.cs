using Domain.Entities;
using Domain.Models;
using Domain.Validation;
using FluentAssertions;

namespace UnitTests.Entities
{
    public class StocksWalletUnitTest
    {
        [Fact]
        public void AddStockMethod_and_BuyMethod_Update_AveragePrice_TotalInvested_CurrentBalance()
        {
            // Arrange
            var stockWalletExample = new StocksWallet();
            var stockExample = new Stock("ITAU", "ITUB4");

            // Act
            stockWalletExample.AddStock(stockExample, 100);
            var stockPrice = stockExample.Price;
            stockExample.UpdatePrice();
            var newStockPrice = stockExample.Price;
            stockWalletExample.BuyStocks(200);
            var averagePrice = Math.Round((stockPrice + (newStockPrice * 2)) / 3, 2);
            var totalInvested = Math.Round((stockPrice * 100) + (newStockPrice * 200), 2);
            var currentBalance = Math.Round(stockExample.Price * 300);

            // Assert
            Assert.Equal(stockWalletExample.AveragePrice, averagePrice);
            Assert.Equal(stockWalletExample.TotalInvestedStock, totalInvested);
            Assert.Equal(stockWalletExample.CurrentInvestedStock, currentBalance);
        }

        [Fact]
        public void AddStockMethod_and_SellMethod_Update_AveragePrice_TotalInvested_CurrentBalance()
        {
            // Arrange
            var stockWalletExample = new StocksWallet();
            var stockExample = new Stock("ITAU", "ITUB4");

            // Act
            stockWalletExample.AddStock(stockExample, 400);
            var valueInvested = stockExample.Price * 400;
            stockExample.UpdatePrice();
            stockWalletExample.SellStocks(200);
            var proportinalSold = (double)200 / 400;
            var totalInvested = valueInvested * proportinalSold;
            var averagePrice = (valueInvested * proportinalSold) / 200;
            var currentBalance = stockExample.Price * 200;

            // Assert
            Assert.Equal(averagePrice, stockWalletExample.AveragePrice);
            Assert.Equal(stockWalletExample.TotalInvestedStock, totalInvested);
            Assert.Equal(stockWalletExample.CurrentInvestedStock, currentBalance);
        }

        [Fact]
        public void SellStockMethod_ShouldThrown_DomainException_When_Quantity_is_Lesser_Than_Zero()
        {
            // Arrange
            var stockWalletExample = new StocksWallet();
            var stockExample = new Stock("ITAU", "ITUB4");

            // Act
            var action = () => stockWalletExample.SellStocks(-100);

            // Assert
            action.Should()
              .Throw<DomainExceptionValidation>()
               .WithMessage("Quantity must be greater than 0");
        }

        [Fact]
        public void SellStockMethod_ShouldThrown_DomainException_When_Quantity_is_Bigger_Than_StockQuantity()
        {
            // Arrange
            var stockWalletExample = new StocksWallet();
            var stockExample = new Stock("ITAU", "ITUB4");

            // Act
            stockWalletExample.AddStock(stockExample, 200);
            var action = () => stockWalletExample.SellStocks(300);

            // Assert
            action.Should()
              .Throw<DomainExceptionValidation>()
               .WithMessage("Quantity must be lower than StockQuantity");
        }
    }
}