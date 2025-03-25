using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Service.BusinessModel
{
    public class QuestionBusiness
    {
        public string QuestionText { get; set; } = null!;
        public string AnswerA { get; set; } = null!;
        public string AnswerB { get; set; } = null!;
        public string AnswerC { get; set; } = null!;
        public string AnswerD { get; set; } = null!;
        public string AnswerCorrect { get; set; } = null!;
    }
}
