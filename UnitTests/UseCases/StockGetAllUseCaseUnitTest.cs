using Application.Shared;
using Application.UseCases.StockContext;
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
    public class StockGetAllUseCaseUnitTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IStockRepository> _stockRepository;
        private readonly Mock<IStockHistoryPriceRepository> _stockHistoryPriceRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<StockGetAllUseCase>> _logger;

        public StockGetAllUseCaseUnitTest()
        {
            _fixture = new Fixture();
            _stockRepository = new Mock<IStockRepository>();
            _stockHistoryPriceRepository = new Mock<IStockHistoryPriceRepository>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<StockGetAllUseCase>>();
        }

        [Fact]
        public async Task ListAllStocks_ExecuteAsyncMethod_Succeed()
        {
            // Arrange
            var stocksResult = new List<Stock>
            {
                { new Stock("Vale", "VALE4") },
                new Stock("Cielo", "CIEL4")
            };
            var stockHistoryPriceResult = _fixture.Create<StockHistoryPrice>();
            _stockRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(stocksResult));
            _mapper.Setup(m => m.Map<StockOutput>(It.IsAny<Stock>())).Returns(_fixture.Create<StockOutput>());

            var useCase = new StockGetAllUseCase(_stockRepository.Object, _mapper.Object, _stockHistoryPriceRepository.Object, _logger.Object);


            // Act
            var result = await useCase.ExecuteAsync(null);

            // Assert
            Assert.IsType<Output<List<StockOutput>>>(result);
        }
    }
}
