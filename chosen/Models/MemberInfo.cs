using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class MemberInfo
    {
        public MemberInfo()
        {
            Commodities = new HashSet<Commodity>();
            DrawRecords = new HashSet<DrawRecord>();
            Payments = new HashSet<Payment>();
            TempStorages = new HashSet<TempStorage>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int MemberId { get; set; }
        public string Email { get; set; } = null!;
        public string MemberName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? EncryptedResetCode { get; set; }
        public string? NewPassword { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? ResetDateTime { get; set; }
        public string? Address { get; set; }
        public DateTime? Birthday { get; set; }
        public int Point { get; set; }

        public virtual ICollection<Commodity> Commodities { get; set; }
        public virtual ICollection<DrawRecord> DrawRecords { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<TempStorage> TempStorages { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
