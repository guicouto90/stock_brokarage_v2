using Application.Shared;
using Application.UseCases.StockContext.Outputs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Validation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.StockContext
{
    public class StockGetByCodeOrNameUseCase : IRequestHandler<string, Output<StockOutput>>
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StockGetByCodeOrNameUseCase> _logger;

        public StockGetByCodeOrNameUseCase(
            IStockRepository stockRepository,
            IMapper mapper,
            ILogger<StockGetByCodeOrNameUseCase> logger)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Output<StockOutput>> ExecuteAsync(string input)
        {
            var output = new Output<StockOutput>();

            try
            {
                var stock = await _stockRepository.GetByCodeOrByNameAsync(input.ToUpper()).ConfigureAwait(false);
                if (stock is null)
                {
                    output.AddErrorMessage("Stock not found");
                    return output;
                }

                stock.UpdatePrice();

                var history = new StockHistoryPrice(stock.Price);
                stock.AddHistory(history);

                await _stockRepository.UpdateAsync(stock);
                output.AddResult(_mapper.Map<StockOutput>(stock));
                return output;
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogError($"StockGetByCodeOrNameUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"{ex.Message}");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError($"StockGetByCodeOrNameUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"Error to get stock {input}");
                return output;
            }
            
        }
    }
}
