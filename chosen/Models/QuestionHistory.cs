using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class QuestionHistory
    {
        public int QuestionHistoryId { get; set; }
        public int? QuestionReportId { get; set; }
        public string? Message { get; set; }
        public int? WhoAnswer { get; set; }
        public DateTime? DateTimeSecond { get; set; }

        public virtual QuestionReport? QuestionReport { get; set; }
    }
}
