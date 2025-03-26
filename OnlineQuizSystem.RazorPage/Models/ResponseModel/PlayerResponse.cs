using OnlineQuiz.Repository.Entities;

namespace OnlineQuizSystem.RazorPage.Models.ResponseModel
{
    public class PlayerResponse
    {
        public int PlayerId { get; set; }

        public int TeamCode { get; set; }

        public string Nickname { get; set; } = null!;

        public int RoomId { get; set; }

        public DateTime? JoinedAt { get; set; }

        public virtual Room Room { get; set; } = null!;
    }
}
