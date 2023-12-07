using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByNumberAsync(int number);
        Task<Account> GetByNumberWithTransactionHistoryAsync(int number);
        Task<Account> GetLastAccountAsync();
        Task<Account> GetByCustomerId(int id);
        Task<Account> GetByCustomerIdWithWalletAsync(int customerId);

    }
}
