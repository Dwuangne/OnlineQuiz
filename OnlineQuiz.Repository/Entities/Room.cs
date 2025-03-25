using System;
using System.Collections.Generic;

namespace OnlineQuiz.Repository.Entities;

public partial class Room
{
    public int RoomId { get; set; }

    public int QuizId { get; set; }

    public int HostId { get; set; }

    public string RoomCode { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    public virtual User Host { get; set; } = null!;

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<QuestionInRoom> QuestionInRooms { get; set; } = new List<QuestionInRoom>();

    public virtual Quiz Quiz { get; set; } = null!;
}
