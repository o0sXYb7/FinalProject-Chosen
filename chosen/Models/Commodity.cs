using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class Commodity
    {
        public Commodity()
        {
            TradeHistories = new HashSet<TradeHistory>();
        }

        public int CommodityId { get; set; }
        public int TempStorageId { get; set; }
        public int MemberId { get; set; }
        public string CommodityName { get; set; } = null!;
        public int? CommodityQuantity { get; set; }
        public int? CommodityUnitPrice { get; set; }
        public DateTime? Deadline { get; set; }
        public bool? OnShelves { get; set; }

        public virtual MemberInfo? Member { get; set; } = null!;
        public virtual TempStorage? TempStorage { get; set; } = null!;
        public virtual ICollection<TradeHistory> TradeHistories { get; set; }
    }
}
