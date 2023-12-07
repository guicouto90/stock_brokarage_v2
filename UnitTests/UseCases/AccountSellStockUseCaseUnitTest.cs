using Moq;
using Application.UseCases.AccountContext;
using Microsoft.Extensions.Logging;
using Domain.Interfaces.Repository;
using Application.UseCases.AccountContext.Inputs;
using Domain.Entities;
using Domain.Models;

namespace UnitTests.UseCases
{
    public class AccountSellStockUseCaseUnitTest
    {
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<IStockRepository> _stockRepository;
        private readonly Mock<ILogger<AccountSellStockUseCase>> _logger;

        public AccountSellStockUseCaseUnitTest()
        {
            _accountRepository = new Mock<IAccountRepository>();
            _stockRepository = new Mock<IStockRepository>();
            _logger = new Mock<ILogger<AccountSellStockUseCase>>();
        }

        [Fact]
        public async Task AccountBuyStock_ExecuteAsyncMethod_Should_Not_Suceed_DueTo_ThereIsNoStockInThisWallet()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            var stockResult = new Stock("TestStock", "STCK4");
            _accountRepository.Setup(x => x.GetByCustomerIdWithWalletAsync(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            _stockRepository.Setup(x => x.GetByCodeOrByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(stockResult));

            var input = new AccountSellStocksInput { CustomerId = customerResult.Id, Quantity = 100, StockCode = "STCK4" };

            var useCase = new AccountSellStockUseCase(_accountRepository.Object, _stockRepository.Object, _logger.Object);

            // Act   
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.Equal("You dont have this stock in your wallet", result.ErrorMessage);
        }

        [Fact]
        public async Task AccountBuyStock_ExecuteAsyncMethod_Should_Not_Suceed_DueTo_QuantityIsBiggerThanStockQuantity()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            customerResult.Account.DepositValue(10000.0);
            var stockResult = new Stock("TestStock", "STCK4");
            customerResult.Account.BuyStock(stockResult, 100);
            _accountRepository.Setup(x => x.GetByCustomerIdWithWalletAsync(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            _stockRepository.Setup(x => x.GetByCodeOrByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(stockResult));

            var input = new AccountSellStocksInput { CustomerId = customerResult.Id, Quantity = 200, StockCode = "STCK4" };

            var useCase = new AccountSellStockUseCase(_accountRepository.Object, _stockRepository.Object, _logger.Object);

            // Act   
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.Equal("Quantity must be lower than StockQuantity", result.ErrorMessage);
        }

        [Fact]
        public async Task AccountBuyStock_ExecuteAsyncMethod_Should_Suceed()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            customerResult.Account.DepositValue(10000.0);
            var stockResult = new Stock("TestStock", "STCK4");
            customerResult.Account.BuyStock(stockResult, 100);
            _accountRepository.Setup(x => x.GetByCustomerIdWithWalletAsync(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            _stockRepository.Setup(x => x.GetByCodeOrByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(stockResult));

            var input = new AccountSellStocksInput { CustomerId = customerResult.Id, Quantity = 100, StockCode = "STCK4" };

            var useCase = new AccountSellStockUseCase(_accountRepository.Object, _stockRepository.Object, _logger.Object);

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.IsType<string>(result.Data);
            Assert.Equal("Sold succeed - Quantity: 100, Stock STCK4", result.Data);
        }
    }
}
