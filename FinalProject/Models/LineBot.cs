using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FinalProject.Models;

namespace FinalProject.Models
{
    public partial class LineBot
    {
        public int LineBotId { get; set; }
        public DateTime SendTime { get; set; }
        public string Message { get; set; } = null!;
        public string MessageType { get; set; } = null!;
    }
}
