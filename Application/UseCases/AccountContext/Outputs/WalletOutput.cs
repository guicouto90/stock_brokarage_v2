namespace Application.UseCases.AccountContext.Outputs
{
    public class WalletOutput
    {
        private double _totalInvested;
        public double TotalInvested
        {
            get { return Math.Round(_totalInvested, 2); }
            set { _totalInvested = value; }
        }

        private double _currentBalance;

        public double CurrentBalance
        {
            get { return Math.Round(_currentBalance, 2); }
            set { _currentBalance = value; }
        }

        public List<StocksWalletOutput> StocksWallet { get; set; }
    }
}
