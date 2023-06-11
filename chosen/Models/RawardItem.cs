using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class RawardItem
    {
        public int RawardId { get; set; }
        public int RawardItemId { get; set; }
        public string Name { get; set; } = null!;
        public string RawardLevel { get; set; } = null!;
        public bool IsJackpot { get; set; }
        public int Num { get; set; }
        public string Image { get; set; } = null!;

        public virtual RawardLib Raward { get; set; } = null!;
    }
}
