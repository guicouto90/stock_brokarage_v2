using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StockHistoryPrice
    {
        public int Id { get; private set; }

        public int StockId { get; private set; }

        public double ActualPrice { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public StockHistoryPrice(int id, double actualPrice, int stockId)
        {
            Id = id;
            ActualPrice = actualPrice;
            UpdatedAt = DateTime.Now;
            StockId = stockId;
        }

        public StockHistoryPrice(double actualPrice)
        {
            ActualPrice = actualPrice;
            UpdatedAt = DateTime.Now;
        }
    }
}
