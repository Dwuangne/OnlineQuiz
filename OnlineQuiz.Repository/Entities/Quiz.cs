using System;
using System.Collections.Generic;

namespace OnlineQuiz.Repository.Entities;

public partial class Quiz
{
    public int QuizId { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual User User { get; set; } = null!;
}
