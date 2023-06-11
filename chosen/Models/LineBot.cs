using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class LineBot
    {
        public int LineBotId { get; set; }
        public DateTime SendTime { get; set; }
        public string Message { get; set; } = null!;
        public string MessageType { get; set; } = null!;
    }
}
