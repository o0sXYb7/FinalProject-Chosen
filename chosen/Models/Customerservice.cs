using System;
using System.Collections.Generic;

namespace chosen.Models
{
    public partial class Customerservice
    {
        public int CustomerserviceId { get; set; }
        public string? Class { get; set; }
        public string? QuestionTitle { get; set; }
        public string? AnswerTitle { get; set; }
    }
}
