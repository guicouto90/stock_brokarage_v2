using Domain.Models;

namespace Domain.Interfaces.Repository
{
    public interface IStockRepository : IRepository<Stock>
    {
        Task<Stock> GetByCodeOrByNameAsync(string input);
    }
}
