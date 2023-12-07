using Application.UseCases.CustomerContext;
using Application.UseCases.CustomerContext.Inputs;
using Application.UseCases.CustomerContext.Outputs;
using AutoFixture;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.UseCases
{
    public class CustomerCreateUseCaseUnitTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<CustomerCreateUseCase>> _logger;

        public CustomerCreateUseCaseUnitTest()
        {
            _fixture = new Fixture();
            _customerRepository = new Mock<ICustomerRepository>();
            _accountRepository = new Mock<IAccountRepository>();
            _logger = new Mock<ILogger<CustomerCreateUseCase>>();
            _mapper = new Mock<IMapper>();
        }


        [Fact]
        public void CustomerCreateUseCase_Creation_Should_Suceed()
        {
            var useCase = new CustomerCreateUseCase(_customerRepository.Object, _accountRepository.Object, _mapper.Object, _logger.Object);
            Assert.NotNull(useCase);
        }

        [Fact]
        public async Task CustomerCreateUseCase_ExecuteAsyncMethod_Should_Suceed()
        {
            // Arrange
            var input = _fixture.Build<CustomerCreateInput>()
                .With(x => x.Name, "Robervaldo")
                .With(x => x.Cpf, "01234567890")
                .With(x => x.Password, "1234567")
                .Create();
            var customer = new Customer(input.Name, input.Cpf, 1, input.Password);

            _customerRepository.Setup(x => x.CreateAsync(It.IsAny<Customer>())).Returns(Task.FromResult(customer));
            
            var expectedOutput = _fixture.Build<CustomerOutput>()
                .With(x => x.Name, customer.Name)
                .With(x => x.Cpf, customer.Cpf)
                .Create();
            _mapper.Setup(m => m.Map<CustomerOutput>(It.IsAny<Customer>())).Returns(expectedOutput);

            var useCase = new CustomerCreateUseCase(_customerRepository.Object, _accountRepository.Object, _mapper.Object, _logger.Object);


            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.IsType<string>(result.Data);
        }
    }
}
