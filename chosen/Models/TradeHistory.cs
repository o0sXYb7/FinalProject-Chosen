using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class TradeHistory
    {
        public int TradeHistoryId { get; set; }
        public int Seller { get; set; }
        public int Buyer { get; set; }
        public int CommodityId { get; set; }
        public int CommodityQuantity { get; set; }
        public int CommodityUnitPrice { get; set; }
        public DateTime TradeTime { get; set; }

        public virtual Commodity Commodity { get; set; } = null!;
    }
}
