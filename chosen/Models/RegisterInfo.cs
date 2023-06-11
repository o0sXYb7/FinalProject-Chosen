using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class RegisterInfo
    {
        public int MemberId { get; set; }
        public string Email { get; set; } = null!;
        public string MemberName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
