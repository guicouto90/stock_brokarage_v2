using Moq;
using Application.UseCases.AccountContext;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using Domain.Models;
using Application.UseCases.AccountContext.Inputs;

namespace UnitTests.UseCases
{
    public class AccountBuyStockUseCaseUnitTest
    {
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<IStockRepository> _stockRepository;
        private readonly Mock<ILogger<AccountBuyStockUseCase>> _logger;
        private readonly AccountBuyStockUseCase _useCase;

        public AccountBuyStockUseCaseUnitTest()
        {
            _accountRepository = new Mock<IAccountRepository>();
            _stockRepository = new Mock<IStockRepository>();
            _logger = new Mock<ILogger<AccountBuyStockUseCase>>();
            _useCase = new AccountBuyStockUseCase(_accountRepository.Object, _stockRepository.Object, _logger.Object);
        }

        [Fact]
        public async Task AccountBuyStock_ExecuteAsyncMethod_Should_Not_Suceed_Due_To_No_Balance()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            var stockResult = new Stock("TestStock", "STCK4");
            _accountRepository.Setup(x => x.GetByCustomerIdWithWalletAsync(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            _stockRepository.Setup(x => x.GetByCodeOrByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(stockResult));

            var input = new AccountBuyStocksInput { CustomerId = customerResult.Id, Quantity = 100, StockCode = "STCK4" };

            // Act  
            var result = await _useCase.ExecuteAsync(input);

            // Assert
            Assert.Equal("There is no enough balance to buy these stocks", result.ErrorMessage);
        }

        [Fact]
        public async Task AccountBuyStock_ExecuteAsyncMethod_Should_Not_Suceed()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            customerResult.Account.DepositValue(10000.0);
            var stockResult = new Stock("TestStock", "STCK4");
            _accountRepository.Setup(x => x.GetByCustomerIdWithWalletAsync(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            _stockRepository.Setup(x => x.GetByCodeOrByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(stockResult));

            var input = new AccountBuyStocksInput { CustomerId = customerResult.Id, Quantity = 100, StockCode = "STCK4" };

            // Act
            var result = await _useCase.ExecuteAsync(input);

            // Assert
            Assert.IsType<string>(result.Data);
            Assert.Equal("Purchase succeed - Quantity: 100, Stock STCK4", result.Data);
        }
    }
}
