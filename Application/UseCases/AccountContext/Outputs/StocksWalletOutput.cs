using Application.UseCases.StockContext.Outputs;

namespace Application.UseCases.AccountContext.Outputs
{
    public class StocksWalletOutput
    {
        private double _averagePrice;
        public double AveragePrice
        {
            get { return Math.Round(_averagePrice, 2); }
            set { _averagePrice = value; }
        }

        public int StockQuantity { get;  set; }

        private double _totalInvestedStock;
        public double TotalInvestedStock
        {
            get { return Math.Round(_totalInvestedStock, 2); }
            set { _totalInvestedStock = value; }
        }

        private double _currentInvestedStock;
        public double CurrentInvestedStock
        {
            get { return Math.Round(_currentInvestedStock, 2); }
            set { _currentInvestedStock = value; }
        }

        public StockOutput Stock { get; set; }
    }
}
