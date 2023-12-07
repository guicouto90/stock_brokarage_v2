using Application.Shared;
using Application.UseCases.AccountContext.Outputs;
using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repository;
using Domain.Validation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.AccountContext
{
    public class AccountStockTransactionsUseCase : IRequestHandler<int, Output<List<TransactionHistoryOutput>>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountStockTransactionsUseCase> _logger;
        public AccountStockTransactionsUseCase(
            IAccountRepository accountRepository, 
            IMapper mapper,
            ILogger<AccountStockTransactionsUseCase> logger)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Output<List<TransactionHistoryOutput>>> ExecuteAsync(int input)
        {
            var output = new Output<List<TransactionHistoryOutput>>();
            try
            {
                var account = await _accountRepository.GetByCustomerId(input);
                if (account is null)
                {
                    _logger.LogError("AccountStockTransactionsUseCase, Error: Account not found");
                    output.AddErrorMessage("Account not found");
                    return output;
                }

                var stockTransactionHistory = account.TransactionHistories
                    .Where(th => th.TypeTransaction == TypeTransaction.BUY_STOCK
                    || th.TypeTransaction == TypeTransaction.SELL_STOCK).ToList();

                var listTransaction = new List<TransactionHistoryOutput>();
                foreach (var transaction in stockTransactionHistory)
                {
                    listTransaction.Add(_mapper.Map<TransactionHistoryOutput>(transaction));
                }

                output.AddResult(listTransaction);

                return output;
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogError($"AccountStockTransactionsUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"{ex.Message}");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AccountStockTransactionsUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"Error to list transaction for customerId: {input}");
                return output;
            }

            
        }
    }
}
