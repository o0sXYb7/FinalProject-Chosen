namespace chosen.ViewModel
{
    public class QuestionHistoryViewModel
    {
        public int QuestionHistoryId { get; set; }
        public int QuestionReportId { get; set; } // 添加QuestionReportID属性
        public string Message { get; set; } = default!;
        public DateTime? DateTimeSecond { get; set; } = default!;
        public int WhoAnswer { get; set; } = default!;
    }
}
