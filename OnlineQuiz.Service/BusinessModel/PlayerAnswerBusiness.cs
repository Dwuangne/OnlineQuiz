using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineQuiz.Repository.Entities;

namespace OnlineQuiz.Service.BusinessModel
{
    public class PlayerAnswerBusiness
    {
        public int PlayerId { get; set; }

        public int QuestionInRoomId { get; set; }

        public string SelectedAnswer { get; set; } = null!;

        public double ResponseTime { get; set; }

        public int Score { get; set; }

        public DateTime? AnsweredAt { get; set; }
    }
}
