using Application.Shared;
using Application.UseCases.StockContext.Inputs;
using Application.UseCases.StockContext.Outputs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Validation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.StockContext
{
    public class StockGetAllUseCase : IRequestHandler<object, Output<List<StockOutput>>>
    {
        private readonly IStockRepository _stockRepository;
        private readonly IStockHistoryPriceRepository _historyPriceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StockGetAllUseCase> _logger;

        public StockGetAllUseCase(
           IStockRepository repository,
           IMapper mapper,
           IStockHistoryPriceRepository stockHistoryPriceRepository,
           ILogger<StockGetAllUseCase> logger
           )
        {
            _stockRepository = repository;
            _mapper = mapper;
            _historyPriceRepository = stockHistoryPriceRepository;
            _logger = logger;
        }
        public async Task<Output<List<StockOutput>>> ExecuteAsync(object? input)
        {
            var output = new Output<List<StockOutput>>();
            try
            {
                var response = await _stockRepository.GetAllAsync().ConfigureAwait(false);
                var listResponse = new List<StockOutput>();
                foreach (var item in response)
                {
                    item.UpdatePrice();

                    var history = new StockHistoryPrice(item.Price);
                    item.AddHistory(history);
                    await _historyPriceRepository.CreateAsync(history).ConfigureAwait(false);
                    await _stockRepository.UpdateAsync(item).ConfigureAwait(false);

                    listResponse.Add(_mapper.Map<StockOutput>(item));
                }
                output.AddResult(listResponse);
                return output;
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogError($"StockGetAllUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"{ex.Message}");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError($"StockGetByCodeOrNameUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"Error to get stocks");
                return output;
            }
        }
    }
}
