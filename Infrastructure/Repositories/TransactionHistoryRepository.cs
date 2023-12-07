using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {
        private readonly StockBrokarageDbContext _context;
        public TransactionHistoryRepository(StockBrokarageDbContext context)
        {
            _context = context;
        }
        public async Task<TransactionHistory> CreateAsync(TransactionHistory entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<TransactionHistory> DeleteAsync(TransactionHistory entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<List<TransactionHistory>> GetAllAsync()
        {
            return await _context.Set<TransactionHistory>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<TransactionHistory> GetByIdAsync(int id)
        {
            return await _context.Set<TransactionHistory>().FirstOrDefaultAsync(th => th.Id == id).ConfigureAwait(false);
        }

        public async Task<List<TransactionHistory>> ListAllByAccountId(int accountId)
        {
            return await _context.Set<TransactionHistory>().Where(th => th.AccountId == accountId).ToListAsync().ConfigureAwait(false);
        }

        public async Task<TransactionHistory> UpdateAsync(TransactionHistory entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }
    }
}
