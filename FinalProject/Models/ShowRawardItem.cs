using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public partial class ShowRawardItem
    {
        public ShowRawardItem()
        {
            TempStorages = new HashSet<TempStorage>();
        }

        public int ShowRawardItemId { get; set; }
        public int ShowRawardId { get; set; }
        [Required(ErrorMessage = "請輸入名稱")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "請輸入級別，例如:【A賞】")]
        public string RawardLevel { get; set; } = null!;
        public bool IsJackpot { get; set; }
        [Range(0, 10000, ErrorMessage = "請輸入獎品數量")]
        public int Num { get; set; }
        [Range(0, 10000, ErrorMessage = "請輸入剩餘獎品數量")]
        public int LaveNum { get; set; }
        [Required(ErrorMessage = "請選擇照片")]
        public string Image { get; set; } = null!;

        public virtual ShowRaward ShowRaward { get; set; } = null!;
        public virtual ICollection<TempStorage> TempStorages { get; set; }
    }
}
