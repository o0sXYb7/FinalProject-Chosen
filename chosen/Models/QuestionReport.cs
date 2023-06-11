using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class QuestionReport
    {
        public QuestionReport()
        {
            QuestionHistories = new HashSet<QuestionHistory>();
        }

        public int QuestionReportId { get; set; }
        public int MemberId { get; set; }
        public string? QuestionTitle { get; set; }
        public string? QuestionType { get; set; }
        public DateTime? DateTime { get; set; }
        public string? State { get; set; }

        public virtual ICollection<QuestionHistory> QuestionHistories { get; set; }
    }
}
