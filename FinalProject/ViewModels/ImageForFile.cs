using System.ComponentModel.DataAnnotations;
using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class ImageForFile
    {
        public int ImageId { get; set; }
        [Required(ErrorMessage = "請輸入系列名稱")]
        public string Series { get; set; } = null!;
        [Required(ErrorMessage = "請輸入名稱")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "請上傳圖片")]
        public IFormFile Image { get; set; } = null!;

    }

    
}
