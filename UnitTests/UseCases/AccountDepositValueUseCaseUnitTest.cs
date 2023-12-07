using Application.UseCases.AccountContext;
using Application.UseCases.AccountContext.Inputs;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.UseCases
{
    public class AccountDepositValueUseCaseUnitTest
    {
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<ILogger<AccountDepositValueUseCase>> _logger;

        public AccountDepositValueUseCaseUnitTest()
        {
            _accountRepository = new Mock<IAccountRepository>();
            _logger = new Mock<ILogger<AccountDepositValueUseCase>>();
        }

        [Fact]
        public async Task AccountDepositValue_ExecuteAsyncMethod_Should_Suceed()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            _accountRepository.Setup(x => x.GetByCustomerId(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            var input = new AccountDepositValueInput { CustomerId = customerResult.Id, Value = 100.0 };

            var useCase = new AccountDepositValueUseCase(_accountRepository.Object, _logger.Object);

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.IsType<string>(result.Data);
            Assert.Equal("Deposit succeed", result.Data);
        }
    }
}
