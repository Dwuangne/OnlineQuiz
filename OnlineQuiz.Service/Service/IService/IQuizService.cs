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
    public interface IQuizService
    {
        Task<(IEnumerable<Quiz>, int PageIndex, int TotalPages, int TotalRecords)> GetAsync(
            Expression<Func<Quiz, bool>> filter = null,
            Func<IQueryable<Quiz>, IOrderedQueryable<Quiz>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null);
        Task<Quiz> InsertAsync(QuizBusiness quizBusiness);
        Task<Quiz?> GetByIdAsync(int id);
        Task<Quiz?> UpdateAsync(int id, QuizBusiness quizBusiness);
        Task<Quiz> DeleteAsync(int id);
    }
}
