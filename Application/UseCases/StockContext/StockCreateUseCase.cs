using Application.UseCases.StockContext.Outputs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Models;
using Application.UseCases.StockContext.Inputs;
using Application.Shared;
using Microsoft.Extensions.Logging;
using Domain.Validation;

namespace Application.UseCases.StockContext
{
    public class StockCreateUseCase : IRequestHandler<StockCreateInput, Output<StockOutput>>
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        ILogger<StockCreateUseCase> _logger;
        public StockCreateUseCase(
            IStockRepository repository, 
            IMapper mapper,
            ILogger<StockCreateUseCase> logger
            )
        {
            _stockRepository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Output<StockOutput>> ExecuteAsync(StockCreateInput input)
        {
            var output = new Output<StockOutput>();
            try
            {
                var isStockExist = await _stockRepository.GetByCodeOrByNameAsync(input.Code);
                if (isStockExist is not null)
                {
                    _logger.LogError($"StockCreateUseCase, Error: Stock already registered");
                    output.AddErrorMessage($"Stock already registered");
                    return output;
                }
                var stock = new Stock(input.Name.ToUpper(), input.Code.ToUpper());
                var history = new StockHistoryPrice(stock.Price);
                stock.AddHistory(history);
                await _stockRepository.CreateAsync(stock).ConfigureAwait(false);

                output.AddResult(_mapper.Map<StockOutput>(stock));
                return output;
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogError($"StockCreateUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"{ex.Message}");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError($"StockCreateUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"Error to add the stock: {input.Code}");
                return output;
            }
        }
    }
}