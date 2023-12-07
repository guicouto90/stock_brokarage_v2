using Application.Shared;
using Application.UseCases.AccountContext.Outputs;
using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Validation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.AccountContext
{
    public class AccountListTransactionHistoryUseCase : IRequestHandler<int, Output<AccountOutput>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountListTransactionHistoryUseCase> _logger;

        public AccountListTransactionHistoryUseCase(
            IAccountRepository accountRepository, IMapper mapper, ILogger<AccountListTransactionHistoryUseCase> logger)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Output<AccountOutput>> ExecuteAsync(int input)
        {
            var output = new Output<AccountOutput>();
            try
            {
                var account = await _accountRepository.GetByCustomerId(input).ConfigureAwait(false);
                if (account is null)
                {
                    _logger.LogError("AccountListTransactionHistoryUseCase, Error: Account not found");
                    output.AddErrorMessage("Account not found");
                    return output;
                }
                output.AddResult(_mapper.Map<AccountOutput>(account));
                return output;

            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogError($"AccountListTransactionHistoryUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"{ex.Message}");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AccountListTransactionHistoryUseCase, Error: {ex.Message}");
                output.AddErrorMessage("Error to List wallet");
                return output;
            }
           
        }
    }
}
