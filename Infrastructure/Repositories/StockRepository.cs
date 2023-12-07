using Domain.Interfaces.Repository;
using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public sealed class StockRepository : IStockRepository
    {
        private readonly StockBrokarageDbContext _context;

        public StockRepository(StockBrokarageDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<Stock> DeleteAsync(Stock entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Set<Stock>().Include(stock => stock.History).ToListAsync().ConfigureAwait(false);
        }

        public async Task<Stock> GetByCodeOrByNameAsync(string input)
        {
            return await _context.Set<Stock>()
                .Include(s => s.History)
                .FirstOrDefaultAsync(e => e.Code == input || e.Name == input)
                .ConfigureAwait(false);
        }

        public async Task<Stock> GetByIdAsync(int id)
        {
            return await _context.Set<Stock>()
                .FirstOrDefaultAsync(e => e.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<Stock> UpdateAsync(Stock entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }
    }
}
