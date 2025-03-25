//using OnlineQuiz.Repository.Entities;

namespace OnlineQuizSystem.RazorPage.Models.RequestModel
{
    public class QuizRequest
    {
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; } = default;
        public virtual List<QuestionRequest> Questions { get; set; } = new List<QuestionRequest>();

    }
}
