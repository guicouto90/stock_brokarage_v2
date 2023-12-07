using Application.Shared;
using Application.UseCases.AccountContext;
using Application.UseCases.AccountContext.Outputs;
using AutoFixture;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.UseCases
{
    public class AccountListTransactionHistoryUseCaseUnitTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<AccountListTransactionHistoryUseCase>> _logger;

        public AccountListTransactionHistoryUseCaseUnitTest()
        {
            _fixture = new Fixture();
            _accountRepository = new Mock<IAccountRepository>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<AccountListTransactionHistoryUseCase>>();
        }

        [Fact]
        public async Task AccountListTransactionsHistory_ExecuteAsyncMethod_Should_Suceed()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            _accountRepository.Setup(x => x.GetByCustomerId(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));

           
            var expectedOutput = _fixture.Build<AccountOutput>()
                .With(x => x.AccountNumber, customerResult.Account.AccountNumber)
                .With(x => x.Balance, customerResult.Account.Balance)
                .Create();
            _mapper.Setup(m => m.Map<AccountOutput>(It.IsAny<Account>())).Returns(expectedOutput);

            var useCase = new AccountListTransactionHistoryUseCase(_accountRepository.Object, _mapper.Object, _logger.Object);

            // Act
            var result = await useCase.ExecuteAsync(1);

            // Assert
            Assert.IsType<Output<AccountOutput>>(result);
            Assert.True(result.IsValid);
        }
    }
}
