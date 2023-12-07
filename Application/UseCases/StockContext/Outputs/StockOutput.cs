using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.StockContext.Outputs
{
    public class StockOutput {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        private double _price;
        public double Price {
            get { return Math.Round(_price, 2); }
            set { _price = value; }
        }
        public ICollection<StockHistoryPriceOutput> History { get; set; }
    }
}
