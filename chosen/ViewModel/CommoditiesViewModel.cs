using System;

namespace chosen.Models
{
    public class CommoditiesViewModel
    {
        public string? ShowRawardItemImage { get; set; } = null!;
        public TempStorage? TempStorage { get; set; }
        public Commodity Commodity { get; set; }

    }
}
