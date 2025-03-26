using OnlineQuiz.Repository.Entities;

namespace OnlineQuizSystem.RazorPage.Models.ResponseModel
{
    public class RoomResponse
    {
        public int RoomId { get; set; }

        public int QuizId { get; set; }

        public int HostId { get; set; }

        public string RoomCode { get; set; } = null!;

        public bool? IsActive { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? EndedAt { get; set; }
        public virtual Quiz Quiz { get; set; } = null!;
    }
}
