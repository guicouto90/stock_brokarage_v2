using Domain.Models;
using Domain.Validation;

namespace Domain.Entities
{
    public class Wallet
    {
        public int Id { get; private set; }

        public int AccountId { get; private set; }

        public double TotalInvested { get; private set; }

        public double CurrentBalance { get; private set; }

        public List<StocksWallet> StocksWallet { get; private set; }

        public Wallet()
        {
            TotalInvested = 0.0;
            CurrentBalance = 0.0;
            StocksWallet = new List<StocksWallet>();
        }

        private void UpdateTotalInvested() => TotalInvested = StocksWallet.Sum(sw => sw.TotalInvestedStock);

        public void UpdateCurrentBalance()
        {
            foreach (var item in StocksWallet)
            {
                item.UpdateCurrentValue();
            }
            CurrentBalance = StocksWallet.Sum(sw => sw.CurrentInvestedStock);
        }

        public void BuyStock(Stock stock, int quantity)
        {
            var stockExist = StocksWallet.Where(sw => sw.StockId == stock.Id).FirstOrDefault();
            double transactionValue = stock.Price * quantity;
            if (stockExist != null)
            {
                stockExist.BuyStocks(quantity);
            }
            else
            {
                var stockWallet = new StocksWallet();
                stockWallet.AddStock(stock, quantity);
                StocksWallet.Add(stockWallet);
            }
            UpdateTotalInvested();
            UpdateCurrentBalance();
        }

        public void SellStock(Stock stock, int quantity)
        {
            var stockExist = StocksWallet.Where(sw => sw.StockId == stock.Id).FirstOrDefault();
            double transactionValue = stock.Price * quantity;
            DomainExceptionValidation.When(stockExist == null, "You dont have this stock in your wallet");
            stockExist.SellStocks(quantity);
            UpdateTotalInvested();
            UpdateCurrentBalance();
        }
    }
}
