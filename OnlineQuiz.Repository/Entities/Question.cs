using System;
using System.Collections.Generic;

namespace OnlineQuiz.Repository.Entities;

public partial class Question
{
    public int QuestionId { get; set; }

    public int QuizId { get; set; }

    public string QuestionText { get; set; } = null!;

    public int? TimeLimit { get; set; }

    public string AnswerA { get; set; } = null!;

    public string AnswerB { get; set; } = null!;

    public string AnswerC { get; set; } = null!;

    public string AnswerD { get; set; } = null!;

    public string AnswerCorrect { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public virtual ICollection<QuestionInRoom> QuestionInRooms { get; set; } = new List<QuestionInRoom>();

    public virtual Quiz Quiz { get; set; } = null!;
}
