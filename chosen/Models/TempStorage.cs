using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class TempStorage
    {
        public TempStorage()
        {
            Commodities = new HashSet<Commodity>();
        }

        public int TempStorageId { get; set; }
        public int MemberId { get; set; }
        public int PrizePoolId { get; set; }
        public int PrizeId { get; set; }
        public string PrizeName { get; set; } = null!;
        public int PrizeQuantity { get; set; }
        public string PrizePicture { get; set; } = null!;
        public DateTime AwardDate { get; set; }
        public DateTime Deadline { get; set; }
        public bool Receive { get; set; }
        public bool Created { get; set; }

        public virtual MemberInfo Member { get; set; } = null!;
        public virtual ShowRawardItem Prize { get; set; } = null!;
        public virtual ShowRaward PrizePool { get; set; } = null!;
        public virtual ICollection<Commodity> Commodities { get; set; }
    }
}
