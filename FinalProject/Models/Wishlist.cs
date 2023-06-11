using System;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public partial class Wishlist
    {
        public int ItemId { get; set; }
        public int PraductId { get; set; }
        public int? CustomerId { get; set; }

        public virtual MemberInfo? Customer { get; set; }
        public virtual ShowRaward Praduct { get; set; } = null!;
    }
}
