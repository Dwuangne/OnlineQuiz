using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineQuiz.Repository.Entities;

namespace OnlineQuiz.Service.BusinessModel
{
    public class PlayerBusiness
    {
        public int? PlayerId { get; set; } // Make nullable for create operations
        public int TeamCode { get; set; }
        public string Nickname { get; set; } = null!;
        public int RoomId { get; set; }
        public DateTime? JoinedAt { get; set; }
    }
}