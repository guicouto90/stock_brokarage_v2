using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly StockBrokarageDbContext _context;

        public AccountRepository(StockBrokarageDbContext context)
        {
            _context = context;
        }


        public async Task<Account> CreateAsync(Account entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<Account> DeleteAsync(Account entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<List<Account>> GetAllAsync()
        {
            return await _context.Set<Account>()
                .ToListAsync()
                 .ConfigureAwait(false); ;
        }

        public async Task<Account> GetByCustomerId(int id)
        {
            return await _context.Set<Account>()
                .Include(a => a.TransactionHistories)
                .FirstOrDefaultAsync(a => a.CustomerId == id)
                .ConfigureAwait(false);
        }

        public async Task<Account> GetByCustomerIdWithWalletAsync(int customerId)
        {
            return await _context.Set<Account>()
                .Include(a => a.Wallet)
                .Include(a => a.Wallet.StocksWallet)
                    .ThenInclude(sw => sw.Stock)
                .FirstOrDefaultAsync(a => a.CustomerId == customerId)
                .ConfigureAwait(false);
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await _context.Set<Account>()
                .FirstOrDefaultAsync(a => a.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<Account> GetByNumberAsync(int number)
        {
            return await _context.Set<Account>()
                .FirstOrDefaultAsync(a => a.AccountNumber == number)
                .ConfigureAwait(false);
        }

        public async Task<Account> GetByNumberWithTransactionHistoryAsync(int number)
        {
            return await _context.Set<Account>()
                .Include(a => a.TransactionHistories)
                .FirstOrDefaultAsync(a => a.AccountNumber == number)
                .ConfigureAwait(false);
        }

        public async Task<Account> GetLastAccountAsync()
        {
            return await _context.Set<Account>()
                .OrderBy(a => a.AccountNumber)
                .LastOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<Account> UpdateAsync(Account entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }
    }
}
