using OnlineQuiz.Repository.Entities;

namespace OnlineQuizSystem.RazorPage.Models.ResponseModel
{
    public class PlayerAnswerResponse
    {
        public int PlayerAnswerId { get; set; }

        public int PlayerId { get; set; }

        public int QuestionInRoomId { get; set; }

        public string SelectedAnswer { get; set; } = null!;

        public double ResponseTime { get; set; }

        public int Score { get; set; }

        public DateTime? AnsweredAt { get; set; }
        public virtual PlayerResponse Player { get; set; } = null!;

        public virtual QuestionInRoomResponse QuestionInRoom { get; set; } = null!;
    }
}
