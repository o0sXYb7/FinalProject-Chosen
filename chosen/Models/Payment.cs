using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class Payment
    {
        public string MerchantOrderNo { get; set; } = null!;
        /// <summary>
        /// foreign key from registerinfo
        /// </summary>
        public string Status { get; set; } = null!;
        public string Amt { get; set; } = null!;
        public string TradeNo { get; set; } = null!;
        public string? Ip { get; set; }
        public string? ItemDesc { get; set; }
        public string PaymentType { get; set; } = null!;
        public string? ExpireDate { get; set; }
        public string? BankCode { get; set; }
        public string? CodeNo { get; set; }
        public string? PayTime { get; set; }
        public string? Card6No { get; set; }
        public string? Card4No { get; set; }
        public string? Exp { get; set; }
        public int? MemberId { get; set; }

        public virtual MemberInfo? Member { get; set; }
    }
}
