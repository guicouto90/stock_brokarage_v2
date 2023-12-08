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
                new Stock("PETROBRAS", "PETR4"),
                new Stock("VALE", "VALE3"),
                new Stock("ITAU", "ITUB4"),
                new Stock("BRADESCO ", "BBDC4"),
                new Stock("AMBEV", "ABEV3"),
                new Stock("BANCO DO BRASIL", "BBAS3"),
                new Stock("B3", "B3SA3"),
            };

            _dbContext.AddRange(stocks);
            _dbContext.SaveChanges();
        }
    }
}
