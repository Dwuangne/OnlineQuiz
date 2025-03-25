using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineQuiz.Repository.Entities;

namespace OnlineQuiz.Service.BusinessModel
{
    public class RoomBusiness
    {
        public int QuizId { get; set; }

        public int HostId { get; set; }

        public string RoomCode { get; set; } = null!;

        public bool? IsActive { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? EndedAt { get; set; }
    }
}
