using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Service.BusinessModel
{
    public class QuizBusiness
    {
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public virtual ICollection<QuestionBusiness> Questions { get; set; } = new List<QuestionBusiness>();
    }
}
