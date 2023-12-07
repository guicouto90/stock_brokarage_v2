using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly StockBrokarageDbContext _context;

        public CustomerRepository(StockBrokarageDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateAsync(Customer entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<Customer> DeleteAsync(Customer entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Set<Customer>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<Customer> GetByCpfAsync(string cpf)
        {
            return await _context.Set<Customer>()
                .Include(e => e.Account)
                .FirstOrDefaultAsync(c => c.Cpf == cpf)
                .ConfigureAwait(false);
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Set<Customer>()
                .Include(e => e.Account)
                .FirstOrDefaultAsync(c => c.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<Customer> UpdateAsync(Customer entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }
    }
}
