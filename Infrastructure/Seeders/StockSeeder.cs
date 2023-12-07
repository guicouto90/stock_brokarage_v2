using Domain.Models;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Seeders
{
    public class StockSeeder
    {
        private readonly StockBrokarageDbContext _dbContext;

        public StockSeeder(StockBrokarageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedData()
        {
            // Create and add seed data to the in-memory database
            var stocks = new List<Stock>
            {
                new Stock("Petrobrás", "PETR4"),
                new Stock("Vale", "VALE3"),
                new Stock("Itau Unibanco", "ITUB4"),
                new Stock("Bradesco ", "BBDC4"),
                new Stock("Ambev", "ABEV3"),
                new Stock("Banco do Brasil", "BBAS3"),
                new Stock("B3", "B3SA3"),
            };

            _dbContext.AddRange(stocks);
            _dbContext.SaveChanges();
        }
    }
}
