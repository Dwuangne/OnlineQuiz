using System;
using System.Collections.Generic;

namespace OnlineQuiz.Repository.Entities;

public partial class PlayerAnswer
{
    public int PlayerAnswerId { get; set; }

    public int PlayerId { get; set; }

    public int QuestionInRoomId { get; set; }

    public string SelectedAnswer { get; set; } = null!;

    public double ResponseTime { get; set; }

    public int Score { get; set; }

    public DateTime? AnsweredAt { get; set; }

    public virtual Player Player { get; set; } = null!;

    public virtual QuestionInRoom QuestionInRoom { get; set; } = null!;
}
