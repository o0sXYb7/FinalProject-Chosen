using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public partial class ShowRaward
    {
        public ShowRaward()
        {
            DrawRecords = new HashSet<DrawRecord>();
            ShowRawardItems = new HashSet<ShowRawardItem>();
            TempStorages = new HashSet<TempStorage>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int ShowRawardId { get; set; }
        [Required(ErrorMessage = "請輸入名稱")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "請輸入系列名稱")]
        public string Series { get; set; } = null!;
        [Range(0, 10, ErrorMessage = "請輸入未中大獎時提升中獎之機率，請介於{1}~{2}之間")]
        public int AddProbability { get; set; }
        public int NowProbability { get; set; }
        [Range(0, 30, ErrorMessage = "允許存放天數只能介於{1}~{2}之間")]
        public int AllowStoreDay { get; set; }
        [Range(0, 100000, ErrorMessage = "運費只能介於{1}~{2}之間")]
        public int Fare { get; set; }
        [Range(1, 10000, ErrorMessage = "請輸入一抽的費用")]
        public int OneDrawMoney { get; set; }
        [Required(ErrorMessage = "請選擇照片")]
        public string Image { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public string? Introduction { get; set; }
        public bool IsOpen { get; set; }
        public string? HasSelectNumber { get; set; }
        [Range(1, 99999999, ErrorMessage = "請選擇廠商")]
        public int FactoryId { get; set; }

        public virtual Factory Factory { get; set; } = null!;
        public virtual ICollection<DrawRecord> DrawRecords { get; set; }
        public virtual ICollection<ShowRawardItem> ShowRawardItems { get; set; }
        public virtual ICollection<TempStorage> TempStorages { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
