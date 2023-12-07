using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetByCpfAsync(string cpf);
    }
}
