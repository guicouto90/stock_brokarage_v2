using Application.Shared;
using Application.UseCases.AccountContext.Inputs;
using Domain.Interfaces.Repository;
using Domain.Validation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.AccountContext
{
    public class AccountWithdrawValueUseCase : IRequestHandler<AccountWithdrawInput, Output<object>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountWithdrawValueUseCase> _logger;

        public AccountWithdrawValueUseCase(
            IAccountRepository accountRepository,
            ILogger<AccountWithdrawValueUseCase> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<Output<object>> ExecuteAsync(AccountWithdrawInput input)
        {
            var output = new Output<object>();
            try
            {
                var account = await _accountRepository.GetByCustomerId(input.CustomerId);
                if (account is null)
                {
                    _logger.LogError("AccountBuyStockUseCase, Error: Stock Not Found");
                    output.AddErrorMessage("Stock Not Found");
                    return output;
                }

                account.WithdrawValue(input.Value);
                await _accountRepository.UpdateAsync(account).ConfigureAwait(false);
                output.AddResult("Withdraw succeed");
                return output;
            } catch(DomainExceptionValidation ex)
            {
                _logger.LogError($"AccountWithdrawValueUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"{ex.Message}");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AccountWithdrawValueUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"Error to withdraw value to customerId: {input.CustomerId}");
                return output;
            }
        }
    }
}
