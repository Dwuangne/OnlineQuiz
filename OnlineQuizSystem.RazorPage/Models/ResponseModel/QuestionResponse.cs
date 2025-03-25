//using OnlineQuiz.Repository.Entities;

namespace OnlineQuizSystem.RazorPage.Models.ResponseModel
{
    public class QuestionResponse
    {
        public int QuestionId { get; set; }
        public int QuizId { get; set; }
        public string QuestionText { get; set; } = null!;
        public int? TimeLimit { get; set; }
        public string AnswerA { get; set; } = null!;
        public string AnswerB { get; set; } = null!;
        public string AnswerC { get; set; } = null!;
        public string AnswerD { get; set; } = null!;
        public string AnswerCorrect { get; set; } = null!;
    }
}
