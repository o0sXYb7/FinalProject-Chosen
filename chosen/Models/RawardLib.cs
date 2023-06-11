using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class RawardLib
    {
        public RawardLib()
        {
            RawardItems = new HashSet<RawardItem>();
        }

        public int RawardId { get; set; }
        public string Series { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<RawardItem> RawardItems { get; set; }
    }
}
