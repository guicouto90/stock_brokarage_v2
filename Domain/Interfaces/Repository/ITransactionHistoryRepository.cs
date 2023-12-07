using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface ITransactionHistoryRepository : IRepository<TransactionHistory>
    {
        Task<List<TransactionHistory>> ListAllByAccountId(int accountId);
    }
}
