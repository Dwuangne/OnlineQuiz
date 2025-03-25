using System;
using System.Collections.Generic;

namespace OnlineQuiz.Repository.Entities;

public partial class Player
{
    public int PlayerId { get; set; }

    public int TeamCode { get; set; }

    public string Nickname { get; set; } = null!;

    public int RoomId { get; set; }

    public DateTime? JoinedAt { get; set; }

    public virtual ICollection<PlayerAnswer> PlayerAnswers { get; set; } = new List<PlayerAnswer>();

    public virtual Room Room { get; set; } = null!;
}
