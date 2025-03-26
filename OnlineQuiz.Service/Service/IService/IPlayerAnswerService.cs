using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Service.Service.IService
{
    public interface IPlayerAnswerService
    {
        Task<(IEnumerable<PlayerAnswer>, int PageIndex, int TotalPages, int TotalRecords)> GetAsync(int roomId, int? pageIndex = null, int? pageSize = null);
        Task<PlayerAnswer> InsertAsync(PlayerAnswerBusiness PlayerAnswerBusiness);
        Task<PlayerAnswer?> GetByIdAsync(int id);
    }
}
