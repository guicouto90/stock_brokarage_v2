using Application.Shared;
using Application.UseCases.AccountContext.Outputs;
using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Validation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.AccountContext
{
    public class AccountListAccountUseCase : IRequestHandler<int, Output<WalletOutput>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountListAccountUseCase> _logger;
        public AccountListAccountUseCase(
            IAccountRepository accountRepository,
            IMapper mapper,
            ILogger<AccountListAccountUseCase> logger)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Output<WalletOutput>> ExecuteAsync(int input)
        {
            var output = new Output<WalletOutput>();
            try
            {
                var account = await _accountRepository.GetByCustomerIdWithWalletAsync(input);
                if (account is null)
                {
                    _logger.LogError("AccountListAccountUseCase, Error: Account not found");
                    output.AddErrorMessage("Account not found");
                    return output;
                }

                account.Wallet.UpdateCurrentBalance();
                await _accountRepository.UpdateAsync(account).ConfigureAwait(false);
                output.AddResult(_mapper.Map<WalletOutput>(account.Wallet));
                return output;
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogError($"AccountListAccountUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"{ex.Message}");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AccountListAccountUseCase, Error: {ex.Message}");
                output.AddErrorMessage("Error to List wallet");
                return output;
            }

        }
    }
}
