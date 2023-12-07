using Application.UseCases.StockContext.Inputs;
using Application.UseCases.StockContext.Outputs;
using Application.Shared;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.StockContext;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {

        private readonly IRequestHandler<StockCreateInput, Output<StockOutput>> _createStockUseCase;
        private readonly IRequestHandler<object, Output<List<StockOutput>>> _getAllStocksUseCase;
        private readonly IRequestHandler<string, Output<StockOutput>> _getStockByNameOrCodeUseCase;

        public StockController(
            IRequestHandler<StockCreateInput, Output<StockOutput>> createStockUseCase,
            IRequestHandler<object, Output<List<StockOutput>>> getAllStocksUseCase,
            IRequestHandler<string, Output<StockOutput>> getStockByNameOrCode
            )
        {
            _createStockUseCase = createStockUseCase;
            _getAllStocksUseCase = getAllStocksUseCase;
            _getStockByNameOrCodeUseCase = getStockByNameOrCode;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] StockCreateInput input)
        {

            var output = await _createStockUseCase.ExecuteAsync(input).ConfigureAwait(false);

            if (!output.IsValid)
                return BadRequest(output.ErrorMessage);

            return Ok(output.Data);

        }

        [HttpGet]
        public async Task<IActionResult> ListAllAsync()
        {
            var output = await _getAllStocksUseCase.ExecuteAsync(default).ConfigureAwait(false);

            if (!output.IsValid) return BadRequest(output.ErrorMessage);

            return Ok(output.Data);

        }

        [HttpGet("code-or-name")]
        [ProducesResponseType(typeof(StockOutput), 200)]
        public async Task<IActionResult> ListAsyncByNameOrCode([FromQuery] string filter)
        {
            var output = await _getStockByNameOrCodeUseCase.ExecuteAsync(filter);

            if (!output.IsValid)
                return BadRequest(output.ErrorMessage);

            return Ok(output.Data);
        }
    }
}
