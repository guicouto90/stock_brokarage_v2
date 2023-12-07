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
    public class StockGetByCodeOrNameUseCaseUnitTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IStockRepository> _stockRepository;
        private readonly Mock<IStockHistoryPriceRepository> _stockHistoryPriceRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<StockGetByCodeOrNameUseCase>> _logger;

        public StockGetByCodeOrNameUseCaseUnitTest()
        {
            _fixture = new Fixture();
            _stockRepository = new Mock<IStockRepository>();
            _stockHistoryPriceRepository = new Mock<IStockHistoryPriceRepository>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<StockGetByCodeOrNameUseCase>>();
        }

        [Fact]
        public async Task ListStockByCodeOrName_ExecuteAsyncMethod_Succeed()
        {
            // Arrange
            Stock stockResult = new Stock("Vale", "VALE4");
            var input = "VALE4"; 

            var stockHistoryPriceResult = _fixture.Create<StockHistoryPrice>();
            _stockRepository.Setup(x => x.GetByCodeOrByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(stockResult));
            var expectedOutput = _fixture.Build<StockOutput>().With(x => x.Name, stockResult.Name).With(x => x.Code, stockResult.Code).Create();
            _mapper.Setup(m => m.Map<StockOutput>(It.IsAny<Stock>())).Returns(expectedOutput);

            var useCase = new StockGetByCodeOrNameUseCase(_stockRepository.Object, _mapper.Object, _logger.Object);


            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.IsType<StockOutput>(result.Data);
            Assert.Equal(stockResult.Name, result.Data.Name);
            Assert.Equal(stockResult.Code, result.Data.Code);
        }

        [Fact]
        public async Task ListStockByCodeOrName_ExecuteAsyncMethod_NotFound_ReturnNull()
        {
            // Arrange
            var input = "VALE5";

            _stockRepository.Setup(x => x.GetByCodeOrByNameAsync(It.IsAny<string>())).Returns(Task.FromResult<Stock>(null));
            var useCase = new StockGetByCodeOrNameUseCase(_stockRepository.Object, _mapper.Object, _logger.Object);

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}
