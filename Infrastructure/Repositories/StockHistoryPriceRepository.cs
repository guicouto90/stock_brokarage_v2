using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class StockHistoryPriceRepository
        : IStockHistoryPriceRepository
    {

        private readonly StockBrokarageDbContext _context;

        public StockHistoryPriceRepository(StockBrokarageDbContext context)
        {
            _context = context;
        }

        public async Task<StockHistoryPrice> CreateAsync(StockHistoryPrice entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<StockHistoryPrice> DeleteAsync(StockHistoryPrice entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }


        public async Task<List<StockHistoryPrice>> GetAllAsync()
        {
            return await _context.Set<StockHistoryPrice>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<StockHistoryPrice> GetByIdAsync(int id)
        {
            return await _context.Set<StockHistoryPrice>()
                 .FirstOrDefaultAsync(c => c.Id == id)
                 .ConfigureAwait(false);
        }

        public async Task<StockHistoryPrice> UpdateAsync(StockHistoryPrice entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }
    }
}
