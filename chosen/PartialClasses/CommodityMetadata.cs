using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace chosen.Models
{
    internal class CommodityMetadata
    {
        [DisplayName("商品ID")]
        public int CommodityId { get; set; }

        [DisplayName("倉庫ID")]
        public int TempStorageId { get; set; }

        [DisplayName("會員ID")]
        public int? MemberId { get; set; }

        [DisplayName("商品名稱")]
        [Required(ErrorMessage = "必填欄位")]
        public string CommodityName { get; set; } = null!;

        [DisplayName("商品數量")]
        [Required(ErrorMessage = "必填欄位")]
        [Range(0, int.MaxValue, ErrorMessage = "商品數量必須大於等於 0。")]
        [CommodityQuantityValidator("TempStorageId", ErrorMessage = "商品數量不能超過 {0}")]
        public int CommodityQuantity { get; set; }

        [DisplayName("商品單價")]
        [Required(ErrorMessage = "必填欄位")]
        public int CommodityUnitPrice { get; set; }

        [DisplayName("截止日期")]
        public DateTime? Deadline { get; set; }

        [DisplayName("販售狀態")]
        public bool? OnShelves { get; set; }
    }
}