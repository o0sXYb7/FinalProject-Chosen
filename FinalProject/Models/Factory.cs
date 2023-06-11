using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public partial class Factory
    {
        public Factory()
        {
            DrawRecords = new HashSet<DrawRecord>();
            ShowRawards = new HashSet<ShowRaward>();
        }

        public int FactoryId { get; set; }
        [Required(ErrorMessage = "請輸入廠商名稱")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "請輸入聯絡電話")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "請輸入聯絡人")]
        public string ContactPerson { get; set; } = null!;

        public virtual ICollection<DrawRecord> DrawRecords { get; set; }
        public virtual ICollection<ShowRaward> ShowRawards { get; set; }
    }
}
