using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace chosen.Models
{
    internal class TempStorageMetadata
    {
        [DisplayName("倉庫ID")]
        public int TempStorageId { get; set; }

        [DisplayName("會員ID")]
        public int? MemberId { get; set; }

        [DisplayName("獎池ID")]
        public int? PrizePoolId { get; set; }

        [DisplayName("一番賞ID")]
        public string? PrizeId { get; set; }

        [DisplayName("名稱")]
        public string PrizeName { get; set; } = null!;

        [DisplayName("數量")]
        public int PrizeQuantity { get; set; }

        [DisplayName("例圖名稱")]
        public string PrizePicture { get; set; } = null!;

        [DisplayName("獲獎日期")]
        public DateTime? AwardDate { get; set; }

        [DisplayName("截止日期")]
        public DateTime? Deadline { get; set; }

        [DisplayName("領取狀態")]
        public bool? Receive { get; set; }

        [DisplayName("已為商品")]
        public bool? Created { get; set; }
    }
}