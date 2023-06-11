using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class ShowRaward
    {
        public ShowRaward()
        {
            DrawRecords = new HashSet<DrawRecord>();
            ShowRawardItems = new HashSet<ShowRawardItem>();
            TempStorages = new HashSet<TempStorage>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int ShowRawardId { get; set; }
        public string Name { get; set; } = null!;
        public string Series { get; set; } = null!;
        public int FactoryId { get; set; }
        public int AddProbability { get; set; }
        public int NowProbability { get; set; }
        public int AllowStoreDay { get; set; }
        public int Fare { get; set; }
        public int OneDrawMoney { get; set; }
        public bool IsOpen { get; set; }
        public DateTime CreateTime { get; set; }
        public string? Introduction { get; set; }
        public string Image { get; set; } = null!;
        public string? HasSelectNumber { get; set; }

        public virtual Factory Factory { get; set; } = null!;
        public virtual ICollection<DrawRecord> DrawRecords { get; set; }
        public virtual ICollection<ShowRawardItem> ShowRawardItems { get; set; }
        public virtual ICollection<TempStorage> TempStorages { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
