using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class Factory
    {
        public Factory()
        {
            DrawRecords = new HashSet<DrawRecord>();
            ShowRawards = new HashSet<ShowRaward>();
        }

        public int FactoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string ContactPerson { get; set; } = null!;

        public virtual ICollection<DrawRecord> DrawRecords { get; set; }
        public virtual ICollection<ShowRaward> ShowRawards { get; set; }
    }
}
