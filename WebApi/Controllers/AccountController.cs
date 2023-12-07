using Application.Shared;
using Application.UseCases.AccountContext.Inputs;
using Application.UseCases.AccountContext.Outputs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IRequestHandler<AccountBuyStocksInput, Output<object>> _buyStockUseCase;
        private readonly IRequestHandler<AccountDepositValueInput, Output<object>> _depositUseCase;
        private readonly IRequestHandler<int, Output<WalletOutput>> _listWalletUseCase;
        private readonly IRequestHandler<int, Output<AccountOutput>> _transactionHistoryUseCase;
        private readonly IRequestHandler<AccountSellStocksInput, Output<object>> _sellStockUseCase;
        private readonly IRequestHandler<int, Output<List<TransactionHistoryOutput>>> _stockTransactionHistoryUseCase;
        private readonly IRequestHandler<AccountWithdrawInput, Output<object>> _withdrawUseCase;

        public AccountController(
            IRequestHandler<AccountBuyStocksInput, Output<object>> buyStockUseCase, 
            IRequestHandler<AccountDepositValueInput, Output<object>> depositUseCase, 
            IRequestHandler<int, Output<WalletOutput>> listWalletUseCase, 
            IRequestHandler<int, Output<AccountOutput>> transactionHistoryUseCase, 
            IRequestHandler<AccountSellStocksInput, Output<object>> sellStockUseCase, 
            IRequestHandler<int, Output<List<TransactionHistoryOutput>>> stockTransactionHistoryUseCase, 
            IRequestHandler<AccountWithdrawInput, Output<object>> withdrawUseCase)
        {
            _buyStockUseCase = buyStockUseCase;
            _depositUseCase = depositUseCase;
            _listWalletUseCase = listWalletUseCase;
            _transactionHistoryUseCase = transactionHistoryUseCase;
            _sellStockUseCase = sellStockUseCase;
            _stockTransactionHistoryUseCase = stockTransactionHistoryUseCase;
            _withdrawUseCase = withdrawUseCase;
        }

        [HttpPut("deposit")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DepositValue([FromBody] AccountDepositValueInput input)
        {
                var customerId = User.Claims.FirstOrDefault(c => c.Type == "customerId").Value;
                input.CustomerId = int.Parse(customerId);
                var output = await _depositUseCase.ExecuteAsync(input).ConfigureAwait(false);
                if (!output.IsValid)
                    return BadRequest(output.ErrorMessage);

                return Ok(output.Data);

        }

        [HttpPut("withdraw")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> WithdrawValue([FromBody] AccountWithdrawInput input)
        {
            var customerId = User.Claims.FirstOrDefault(c => c.Type == "customerId").Value;
            input.CustomerId = int.Parse(customerId);
            var output = await _withdrawUseCase.ExecuteAsync(input).ConfigureAwait(false);
            if (!output.IsValid)
                return BadRequest(output.ErrorMessage);

            return Ok(output.Data);
        }

        [HttpGet("transaction-history")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> TransactionHistory()
        {
            var customerId = User.Claims.FirstOrDefault(c => c.Type == "customerId").Value;
            var output = await _transactionHistoryUseCase.ExecuteAsync(int.Parse(customerId)).ConfigureAwait(false);
            if (!output.IsValid)
                return BadRequest(output.ErrorMessage);

            return Ok(output.Data);
        }

        [HttpGet("stock-transaction-history")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> StockTransactionHistory()
        {
            var customerId = User.Claims.FirstOrDefault(c => c.Type == "customerId").Value;
            var output = await _stockTransactionHistoryUseCase.ExecuteAsync(int.Parse(customerId)).ConfigureAwait(false);
            if (!output.IsValid)
                return BadRequest(output.ErrorMessage);

            return Ok(output.Data);
        }

        [HttpGet("wallet")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ListWallet()
        {

            var customerId = User.Claims.FirstOrDefault(c => c.Type == "customerId").Value;
            var output = await _listWalletUseCase.ExecuteAsync(int.Parse(customerId)).ConfigureAwait(false);
            if (!output.IsValid)
                return BadRequest(output.ErrorMessage);

            return Ok(output.Data);
        }

        [HttpPut("buy-stock")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> BuyStocks([FromBody] AccountBuyStocksInput input)
        {
            var customerId = User.Claims.FirstOrDefault(c => c.Type == "customerId").Value;
            input.CustomerId = int.Parse(customerId);
            var output = await _buyStockUseCase.ExecuteAsync(input).ConfigureAwait(false);
            if (!output.IsValid)
                return BadRequest(output.ErrorMessage);

            return Ok(output.Data);
        }

        [HttpPut("sell-stock")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SellStocks([FromBody] AccountSellStocksInput input)
        {
            var customerId = User.Claims.FirstOrDefault(c => c.Type == "customerId").Value;
            input.CustomerId = int.Parse(customerId);
            var output = await _sellStockUseCase.ExecuteAsync(input).ConfigureAwait(false);
            if (!output.IsValid)
                return BadRequest(output.ErrorMessage);

            return Ok(output.Data);
        }
    }
}
