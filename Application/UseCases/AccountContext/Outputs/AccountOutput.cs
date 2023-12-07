using Domain.Entities;

namespace Application.UseCases.AccountContext.Outputs
{
    public class AccountOutput
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public int AccountNumber { get; set; }

        private double _balance;
        public double Balance
        {
            get { return Math.Round(_balance, 2); }
            set { _balance = value; }
        }

        public ICollection<TransactionHistoryOutput> TransactionHistories { get; set; }

    }
}
