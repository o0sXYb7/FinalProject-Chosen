namespace chosen.ViewModel
{
    public class SubmitQuestionViewModel
    {
        public int QuestionReportID { get; set; } = default!;
        public int MemberID { get; set; } = default!;
        public string QuestionTitle { get; set; } = default!;
        public string QuestionType { get; set; } = default!;

        public DateTime QuestionDate { get; set; } = default!;
        public string State { get; set; } = default!;
        public List<QuestionHistoryViewModel> QuestionhistoriesList { get; set; } = new List<QuestionHistoryViewModel>();
    }
}
