namespace Application.UseCases.StockContext
{
    public class StockHistoryPriceOutput
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        private double _actualPrice;
        public double ActualPrice { 
            get { return Math.Round(_actualPrice, 2); }
            set { _actualPrice = value; } 
        }

        public DateTime UpdatedAt { get; set; }
    }
}
