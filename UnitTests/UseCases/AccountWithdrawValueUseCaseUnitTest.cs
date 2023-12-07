using Application.UseCases.AccountContext;
using Application.UseCases.AccountContext.Inputs;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.UseCases
{
    public class AccountWithdrawValueUseCaseUnitTest
    {
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<ILogger<AccountWithdrawValueUseCase>> _logger;

        public AccountWithdrawValueUseCaseUnitTest()
        {
            _accountRepository = new Mock<IAccountRepository>();
            _logger = new Mock<ILogger<AccountWithdrawValueUseCase>>();
        }

        [Fact]
        public async Task AccountWithdrawValue_ExecuteAsyncMethod_Should_Not_Suceed_Due_To_No_Balance()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            _accountRepository.Setup(x => x.GetByCustomerId(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            var input = new AccountWithdrawInput { CustomerId = customerResult.Id, Value = 100.0 };

            var useCase = new AccountWithdrawValueUseCase(_accountRepository.Object, _logger.Object);

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("There is no enough balance to withdraw", result.ErrorMessage);
        }

        [Fact]
        public async Task AccountWithdrawValue_ExecuteAsyncMethod_Should_Suceed()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            customerResult.Account.DepositValue(100);
            _accountRepository.Setup(x => x.GetByCustomerId(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            var input = new AccountWithdrawInput { CustomerId = customerResult.Id, Value = 100.0 };

            var useCase = new AccountWithdrawValueUseCase(_accountRepository.Object, _logger.Object);

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal("Withdraw succeed", result.Data);
        }
    }
}
