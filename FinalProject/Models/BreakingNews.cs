using System;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public partial class BreakingNews
    {
        public int BreakingNewsId { get; set; }
        public string? Title { get; set; }
        public string? ActivityUrl { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? IndexDescription { get; set; }
    }
}
