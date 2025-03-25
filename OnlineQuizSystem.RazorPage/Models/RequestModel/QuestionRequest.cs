//using OnlineQuiz.Repository.Entities;

namespace OnlineQuizSystem.RazorPage.Models.RequestModel
{
    public class QuestionRequest
    {
        public string QuestionText { get; set; } = null!;
        public string AnswerA { get; set; } = null!;
        public string AnswerB { get; set; } = null!;
        public string AnswerC { get; set; } = null!;
        public string AnswerD { get; set; } = null!;
        public string AnswerCorrect { get; set; } = null!;
        public bool? IsDeleted { get; set; } = default;
    }
}
