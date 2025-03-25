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
    public interface IQuestionService
    {
        Task<(IEnumerable<Question>, int PageIndex, int TotalPages, int TotalRecords)> GetAsync(
            int quizId);
        Task<Question> InsertAsync(int quizId, QuestionBusiness questionBusiness);
        Task<Question?> GetByIdAsync(int id);
        Task<Question?> UpdateAsync(int id, QuestionBusiness questionBusiness);
        Task<Question> DeleteAsync(int id);
    }
}
