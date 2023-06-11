using System;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public partial class DrawRecord
    {
        public int DrawId { get; set; }
        public int ShowRawardId { get; set; }
        public int FactoryId { get; set; }
        public int MemberId { get; set; }
        public int DrawCount { get; set; }
        public int Point { get; set; }
        public DateTime DrawTime { get; set; }
        public bool Settlement { get; set; }
        public DateTime? SettlementTime { get; set; }

        public virtual Factory Factory { get; set; } = null!;
        public virtual MemberInfo Member { get; set; } = null!;
        public virtual ShowRaward ShowRaward { get; set; } = null!;
    }
}
