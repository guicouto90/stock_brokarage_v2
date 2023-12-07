using Application.UseCases.CustomerContext;
using Application.UseCases.StockContext;
using Application.UseCases.StockContext.Inputs;
using Application.UseCases.StockContext.Outputs;
using AutoFixture;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.UseCases
{
    public class StockCreateUseCaseUnitTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IStockRepository> _stockRepository;
        private readonly Mock<IStockHistoryPriceRepository> _stockHistoryPriceRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<StockCreateUseCase>> _logger;

        public StockCreateUseCaseUnitTest()
        {
            _fixture = new Fixture();
            _stockRepository = new Mock<IStockRepository>();
            _stockHistoryPriceRepository = new Mock<IStockHistoryPriceRepository>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<StockCreateUseCase>>();
        }

        [Fact]
        public async Task StockCreateUseCase_Creation_Should_Suceed()
        {
            var useCase = new StockCreateUseCase(_stockRepository.Object, _mapper.Object, _logger.Object);
           

            Assert.NotNull(useCase);
        }

        [Fact]
        public async Task StockCreateUseCase_ExecuteAsyncMethod_Should_Suceed()
        {
            // Arrange
            var input = _fixture.Build<StockCreateInput>().With(x => x.Name, "Vale").With(x => x.Code, "VALE4").Create();
            var stockResult = new Stock(input.Name, input.Code);

            var stockHistoryPriceResult = new StockHistoryPrice(stockResult.Price);
            stockResult.AddHistory(stockHistoryPriceResult);
            _stockRepository.Setup(x => x.CreateAsync(It.IsAny<Stock>())).Returns(Task.FromResult(stockResult));
            _stockHistoryPriceRepository.Setup(x => x.CreateAsync(It.IsAny<StockHistoryPrice>())).Returns(Task.FromResult(stockHistoryPriceResult));

            var expectedOutput = _fixture.Build<StockOutput>().With(x => x.Name, stockResult.Name).With(x => x.Code, stockResult.Code).With(x => x.Price, stockResult.Price).Create();
            _mapper.Setup(m => m.Map<StockOutput>(It.IsAny<Stock>())).Returns(expectedOutput);

            var useCase = new StockCreateUseCase(_stockRepository.Object, _mapper.Object, _logger.Object);
            

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.IsType<StockOutput>(result.Data);
            Assert.Equal(stockResult.Name, result.Data.Name);
            Assert.Equal(stockResult.Code, result.Data.Code);
            Assert.Equal(stockResult.Price, result.Data.Price);
        }
    }
}
