using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FinalProject.Models;

namespace FinalProject.Models
{
    public partial class RawardItem
    {
        public int RawardId { get; set; }
        public int RawardItemId { get; set; }
        [Required(ErrorMessage = "請輸入名稱")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "請輸入獎品等級，例如:【A賞】")]
        public string RawardLevel { get; set; } = null!;
        public bool IsJackpot { get; set; }

        //這邊到時候要建立 MetaData 製作判斷訊息 
        [Range(1, 1000, ErrorMessage = "請輸入數量，介於{1}~{2}之間")]
        public int Num { get; set; }
        [Required(ErrorMessage = "請選擇照片")]
        public string Image { get; set; } = null!;

        public virtual RawardLib Raward { get; set; } = null!;
    }
}
