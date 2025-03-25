using System;
using System.Collections.Generic;

namespace OnlineQuiz.Repository.Entities;

public partial class QuestionInRoom
{
    public int QuestionInRoomId { get; set; }

    public int QuestionId { get; set; }

    public int RoomId { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public virtual ICollection<PlayerAnswer> PlayerAnswers { get; set; } = new List<PlayerAnswer>();

    public virtual Question Question { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
