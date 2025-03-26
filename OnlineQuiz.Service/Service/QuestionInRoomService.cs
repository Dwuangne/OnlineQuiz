using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Repository.UnitOfWork;
using OnlineQuiz.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Service.Service
{
    public class QuestionInRoomService : IQuestionInRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionInRoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(IEnumerable<QuestionInRoom>, int PageIndex, int TotalPages, int TotalRecords)> GetQuestionsByRoomIdAsync(int roomId)
        {
            return await _unitOfWork.QuestionInRoomRepository.GetAsync(
               filter: q => q.RoomId == roomId,
               orderBy: null,
               includeProperties: "Question",
               pageIndex: 1,
               pageSize: 10000
               );
        }
    }
}
