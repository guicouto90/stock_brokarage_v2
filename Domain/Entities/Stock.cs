using Domain.Entities;
using Domain.Validation;

namespace Domain.Models
{
    public class Stock
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public string Code { get; private set; }

        public double Price { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public List<StockHistoryPrice> History { get; private set; }

        public Stock(string name, string code)
        {
            ValidateName(name);
            ValidateCode(code);
            Name = name;
            Code = code;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Price = GeneratePriceRandomly();
            History = new List<StockHistoryPrice>();
        }

        public Stock(int id, string name, string code)
        {
            ValidateName(name);
            ValidateCode(code);
            Id = id;
            Name = name;
            Code = code;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Price = GeneratePriceRandomly();
            History = new List<StockHistoryPrice>();
        }

        private static double GeneratePriceRandomly()
        {
            Random random = new Random();
            double price = random.NextDouble() * (50.0 - 1.0) + 1.0;
            return Math.Round(price, 2);
        }

        public void UpdatePrice()
        {
            Price = GeneratePriceRandomly();
            UpdatedAt = DateTime.Now;
        }

        public void AddHistory(StockHistoryPrice history) { History.Add(history); }

        private static void ValidateName(string name)
        {
            DomainExceptionValidation.When(name.Length < 2, "Name length must be equal or greater than 2");
        }

        private static void ValidateCode(string code)
        {
            DomainExceptionValidation.When(code.Length != 5, "Code length must be equal 5");
        }
    }
}
