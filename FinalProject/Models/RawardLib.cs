using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public partial class RawardLib
    {
        public RawardLib()
        {
            RawardItems = new HashSet<RawardItem>();
        }

        public int RawardId { get; set; }
        [Required(ErrorMessage = "請輸入系列名稱")]
        public string Series { get; set; } = null!;
        [Required(ErrorMessage = "請輸入一番賞名稱")]
        public string Name { get; set; } = null!;

        public virtual ICollection<RawardItem> RawardItems { get; set; }
    }
}
