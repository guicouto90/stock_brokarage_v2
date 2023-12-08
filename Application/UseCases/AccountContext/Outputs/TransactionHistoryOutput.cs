using Domain.Entities.Enums;
using System.Text.Json.Serialization;

namespace Application.UseCases.AccountContext.Outputs
{
    public class TransactionHistoryOutput
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TypeTransaction TypeTransaction { get; set; }

        private double? _transactionValue;
        public double TransactionValue
        {
            get { return Math.Round(_transactionValue.GetValueOrDefault(), 2); }
            set { _transactionValue = value; }
        }

        public string? StockCode { get; set; }

        public int? StockQuantity { get; set; }

        private double? _stockPrice;

        public double? StockPrice
        {
            get { return _stockPrice is not null ? Math.Round(_stockPrice.GetValueOrDefault(), 2) : null; }
            set { _stockPrice = value; }
        }

        public DateTime Date { get; set; }

    }
}
