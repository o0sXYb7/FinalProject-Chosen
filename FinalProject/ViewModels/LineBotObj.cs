using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels
{
    public class LineBotObj
    {
        [Required(ErrorMessage = "請輸入訊息類別")]
        public string messageType { get; set; }
        [Required(ErrorMessage = "請輸入內容")]
        public object Messages { get; set; }
    }
}
