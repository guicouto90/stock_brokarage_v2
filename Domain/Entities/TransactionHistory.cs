using Domain.Entities.Enums;

namespace Domain.Entities
{
    public class TransactionHistory
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public TypeTransaction TypeTransaction { get; }

        public double TransactionValue { get; }

        public string? StockCode { get; }

        public double? StockPrice { get; }

        public int? StockQuantity { get; }

        public DateTime Date { get; }

        public TransactionHistory(TypeTransaction typeTransaction, double transactionValue)
        {
            TypeTransaction = typeTransaction;
            TransactionValue = transactionValue;
            Date = DateTime.Now;
        }

        public TransactionHistory(TypeTransaction typeTransaction, double transactionValue, string stockCode, int stockQuantity, double stockPrice)
        {
            TypeTransaction = typeTransaction;
            TransactionValue = transactionValue;
            Date = DateTime.Now;
            StockCode = stockCode;
            StockQuantity = stockQuantity;
            StockPrice = stockPrice;
        }
    }
}
