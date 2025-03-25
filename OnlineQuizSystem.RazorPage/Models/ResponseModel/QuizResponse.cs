using OnlineQuizSystem.RazorPage.Models.RequestModel;

namespace OnlineQuizSystem.RazorPage.Models.ResponseModel
{
    public class QuizResponse
    {
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual ICollection<QuestionResponse> Questions { get; set; } = new List<QuestionResponse>();

    }
}
