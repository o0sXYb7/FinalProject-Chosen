using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace chosen.Models
{
    public class TempStoragesViewModel
    {
        public int TempStorageId { get; set; }
        public int MemberId { get; set; }
        public string? PrizeName { get; set; } = null!;
        public int? PrizeQuantity { get; set; }
        public string? ShowRawardName { get; set; } = null!;
        public DateTime Deadline { get; set; }
        public TempStorage? TempStorage { get; set; }
        public Commodity Commodity { get; set; }
    }
}
