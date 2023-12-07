using Application.UseCases.AccountContext.Outputs;

namespace Application.UseCases.CustomerContext.Outputs
{
    public class CustomerOutput
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public string Cpf { get; set; }
        public int AccountId { get;  set; }
        public AccountOutput Account { get;  set; }
    }
}
