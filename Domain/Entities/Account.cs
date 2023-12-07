using Domain.Entities.Enums;
using Domain.Models;
using Domain.Validation;

namespace Domain.Entities
{
    public sealed class Account
    {
        public int Id { get; set; }
        public int CustomerId { get; private set; }

        public Customer Customer { get; private set; }
        public int AccountNumber { get; }
        public double Balance { get; private set; }
        public string Password { get; private set; }

        public Wallet Wallet { get; private set; }

        public List<TransactionHistory> TransactionHistories { get; private set; }

        public Account(int accountNumber, double balance, string password)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Password = password;
            TransactionHistories = new List<TransactionHistory>();
            Wallet = new Wallet();
        }

        public Account(int accountNumber, string password, Customer customer)
        {
            AccountNumber = accountNumber;
            Password = EncryptPassword(password);
            Balance = 0.0;
            TransactionHistories = new List<TransactionHistory>();
            Customer = customer;
            Wallet = new Wallet();
        }

        public void DepositValue(double amount)
        {
            DomainExceptionValidation.When(amount <= 0, "Amount must be bigger than 0");
            AddTransactionHistory(new TransactionHistory(TypeTransaction.DEPOSIT, amount));
            Balance += amount;
        }

        public void WithdrawValue(double amount)
        {
            DomainExceptionValidation.When(amount > Balance, "There is no enough balance to withdraw");
            DomainExceptionValidation.When(amount <= 0, "Amount must be bigger than 0");
            AddTransactionHistory(new TransactionHistory(TypeTransaction.WITHDRAW, -amount));
            Balance -= amount;
        }

        public void BuyStock(Stock stock, int quantity)
        {
            double amount = quantity * stock.Price;
            DomainExceptionValidation.When(Balance < amount, "There is no enough balance to buy these stocks");
            DomainExceptionValidation.When(quantity <= 0, "Quantity must be bigger than 0");
            Wallet.BuyStock(stock, quantity);
            AddTransactionHistory(new TransactionHistory(Enums.TypeTransaction.BUY_STOCK, -amount, stock.Code, quantity, stock.Price));
            Balance -= amount;
        }

        public void SellStock(Stock stock, int quantity)
        {
            DomainExceptionValidation.When(quantity <= 0, "Quantity must be bigger than 0");
            double amount = quantity * stock.Price;
            Wallet.SellStock(stock, quantity);
            AddTransactionHistory(new TransactionHistory(Enums.TypeTransaction.SELL_STOCK, amount, stock.Code, quantity, stock.Price));
            Balance += amount;
        }

        private string EncryptPassword(string password)
        {
            // Hash the password using bcrypt with a cost factor of 12
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, 12);

            return hashedPassword;
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }

        private void AddTransactionHistory(TransactionHistory history) { TransactionHistories.Add(history); }
    }
}
