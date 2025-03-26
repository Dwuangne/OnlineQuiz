using OnlineQuiz.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Service.Service.IService
{
    public interface IQuestionInRoomService
    {
        Task<(IEnumerable<QuestionInRoom>, int PageIndex, int TotalPages, int TotalRecords)> GetQuestionsByRoomIdAsync(
            int roomId);
    }
}
