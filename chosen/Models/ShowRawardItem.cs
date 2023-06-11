using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class ShowRawardItem
    {
        public ShowRawardItem()
        {
            TempStorages = new HashSet<TempStorage>();
        }

        public int ShowRawardItemId { get; set; }
        public int ShowRawardId { get; set; }
        public string Name { get; set; } = null!;
        public string RawardLevel { get; set; } = null!;
        public bool IsJackpot { get; set; }
        public int Num { get; set; }
        public int LaveNum { get; set; }
        public string Image { get; set; } = null!;

        public virtual ShowRaward ShowRaward { get; set; } = null!;
        public virtual ICollection<TempStorage> TempStorages { get; set; }
    }
}
