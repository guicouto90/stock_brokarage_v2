using Domain.Models;
using Domain.Validation;
using FluentAssertions;

namespace UnitTests.Entities
{
    public class StockUnitTest
    {
        [Fact(DisplayName = "Create Stock With Valid State")]
        public void CreateStock_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Stock("ITAU", "ITUB4");
            action.Should()
                 .NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Stock With Invalid Name")]
        public void CreateStock_ShortNameValue_DomainExceptionInvalidId()
        {
            Action action = () => new Stock("I", "ITUB4");
            action.Should()
                .Throw<DomainExceptionValidation>()
                 .WithMessage("Name length must be equal or greater than 2");
        }
        
        [Fact(DisplayName = "Create Stock With Invalid Code")]
        public void CreateStock_ShortCodeValue_DomainExceptionShortName()
        {
            Action action = () => new Stock("ITAU", "ITUB");
            action.Should()
                .Throw<DomainExceptionValidation>()
                   .WithMessage("Code length must be equal 5");
        }
        
        [Fact(DisplayName = "Update Price Method, must update prce value randomly, with value between 1.0 and 50.0")]
        public void UpdatePriceMethod_UpdatePriceValueRandomly_WithValue_BetweenOneAndFifty()
        {
            // Arrange
            var stock = new Stock("ITAU", "ITUB4");
            var oldPrice = stock.Price;
            
            // Act
            stock.UpdatePrice();

            // Assert
            Assert.NotEqual(oldPrice, stock.Price);
            Assert.InRange(stock.Price, 1.0, 50.0);

        }
    }
}
