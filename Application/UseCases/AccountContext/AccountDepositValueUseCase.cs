using Application.Shared;
using Application.UseCases.AccountContext.Inputs;
using Domain.Interfaces.Repository;
using Domain.Validation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.AccountContext
{
    public class AccountDepositValueUseCase : IRequestHandler<AccountDepositValueInput, Output<object>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountDepositValueUseCase> _logger;
        public AccountDepositValueUseCase(
            IAccountRepository accountRepository,
            ILogger<AccountDepositValueUseCase> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<Output<object>> ExecuteAsync(AccountDepositValueInput input)
        {
            var output = new Output<object>();
            try
            {
                var account = await _accountRepository.GetByCustomerId(input.CustomerId);
                if (account is null)
                {
                    _logger.LogError($"AccountDepositValueUseCase, Error: Account not found");
                    output.AddErrorMessage("Account not found");
                    return output;
                }

                account.DepositValue(input.Value);
                await _accountRepository.UpdateAsync(account).ConfigureAwait(false);

                output.AddResult("Deposit succeed");
                return output;
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogError($"AccountDepositValueUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"{ex.Message}");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AccountDepositValueUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"Error to deposit value to customer id: {input.CustomerId}");
                return output;
            }
        }
    }
}
