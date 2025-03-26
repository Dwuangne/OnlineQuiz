using OnlineQuiz.Repository.Entities;

namespace OnlineQuizSystem.RazorPage.Models.ResponseModel
{
    public class QuestionInRoomResponse
    {
        public int QuestionInRoomId { get; set; }
        public int QuestionId { get; set; }
        public int RoomId { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public virtual QuestionResponse Question { get; set; } = null!;
        public virtual RoomResponse Room { get; set; } = null!;
    }
}
