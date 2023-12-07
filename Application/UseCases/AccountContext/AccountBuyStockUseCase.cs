using Application.Shared;
using Application.UseCases.AccountContext.Inputs;
using Domain.Interfaces.Repository;
using Domain.Validation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.AccountContext
{
    public class AccountBuyStockUseCase : IRequestHandler<AccountBuyStocksInput, Output<object>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<AccountBuyStockUseCase> _logger;

        public AccountBuyStockUseCase(
            IAccountRepository accountRepository,
            IStockRepository stockRepository,
            ILogger<AccountBuyStockUseCase> logger)
        {
            _accountRepository = accountRepository;
            _stockRepository = stockRepository;
            _logger = logger;
        }

        public async Task<Output<object>> ExecuteAsync(AccountBuyStocksInput input)
        {
            var output = new Output<object>();
            try
            {
                var account = await _accountRepository.GetByCustomerIdWithWalletAsync(input.CustomerId);
                var stock = await _stockRepository.GetByCodeOrByNameAsync(input.StockCode);
                if (stock is null)
                {
                    _logger.LogError("AccountBuyStockUseCase, Error: Stock Not Found");
                    output.AddErrorMessage("Stock Not Found");
                    return output;
                }

                account.BuyStock(stock, input.Quantity);
                await _accountRepository.UpdateAsync(account).ConfigureAwait(false);

                output.AddResult($"Purchase succeed - Quantity: {input.Quantity}, Stock {stock.Code}");
                return output;

            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogError($"AccountBuyStockUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"{ex.Message}");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AccountBuyStockUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"Error to buy stock: {input.StockCode}");
                return output;
            }
        }
    }
}
