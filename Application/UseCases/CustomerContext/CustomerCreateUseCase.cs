using Application.Shared;
using Application.UseCases.CustomerContext.Inputs;
using Application.UseCases.CustomerContext.Outputs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Validation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.CustomerContext
{
    public class CustomerCreateUseCase : IRequestHandler<CustomerCreateInput, Output<object>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerCreateUseCase> _logger;

        public CustomerCreateUseCase(
            ICustomerRepository customerRepository, 
            IAccountRepository accountRepository, 
            IMapper mapper,
            ILogger<CustomerCreateUseCase> logger)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Output<object>> ExecuteAsync(CustomerCreateInput input)
        {        
            var output = new Output<object>();
            try
            {
                var customerExist = await _customerRepository.GetByCpfAsync(input.Cpf);
                if (customerExist is not null)
                {
                    output.AddErrorMessage("Customer already has an account");
                    return output;
                }
                var lastAccount = await _accountRepository.GetLastAccountAsync().ConfigureAwait(false);
                var accountNumber = lastAccount == null ? 1 : lastAccount.AccountNumber + 1;
                var customer = new Customer(input.Name, input.Cpf, accountNumber, input.Password);
                await _customerRepository.CreateAsync(customer).ConfigureAwait(false);
                output.AddResult($"Account number { accountNumber } created to customer CPF: {input.Cpf}");

                return output;
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogError($"CustomerCreateUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"{ex.Message}");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomerCreateUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"Error to register customer with CPF: {input.Cpf}");
                return output;
            }
        }
    }
}
