using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Repository.UnitOfWork;
using OnlineQuiz.Service.BusinessModel;
using OnlineQuiz.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Service.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Question> DeleteAsync(int id)
        {
            var question = await GetByIdAsync(id);
            if (question == null)
            {
                return null;
            }
            question.IsDeleted = true;
            var questionEntity = await _unitOfWork.QuestionRepository.UpdateAsync(question);
            await _unitOfWork.SaveChangesAsync();
            return questionEntity;
        }

        public async Task<(IEnumerable<Question>, int PageIndex, int TotalPages, int TotalRecords)> GetAsync(int quizId)
        {
            Expression<Func<Question, bool>> filter = s => s.QuizId == quizId && (bool)!s.IsDeleted;
            Func<IQueryable<Question>, IOrderedQueryable<Question>> orderBy = a => a.OrderBy(t => t.QuestionId);
            return await _unitOfWork.QuestionRepository.GetAsync(filter, orderBy, "", 1, 10);
        }

        public async Task<Question?> GetByIdAsync(int id)
        {
            var entities = await _unitOfWork.QuestionRepository.GetAsync(
              filter: q => q.QuestionId == id,
              orderBy: null,
              includeProperties: "",
              pageIndex: 1,
              pageSize: 1
            );
            var entity = entities.Item1.FirstOrDefault();
            return entity;
        }

        public async Task<Question> InsertAsync(int quizId, QuestionBusiness questionBusiness)
        {
            var questionEntity = new Question
            {
                QuizId = quizId,
                QuestionText = questionBusiness.QuestionText,
                AnswerA = questionBusiness.AnswerA,
                AnswerB = questionBusiness.AnswerB,
                AnswerC = questionBusiness.AnswerC,
                AnswerD = questionBusiness.AnswerD,
                AnswerCorrect = questionBusiness.AnswerCorrect,
            };
            var question = await _unitOfWork.QuestionRepository.InsertAsync(questionEntity);
            await _unitOfWork.SaveChangesAsync();
            return question;
        }

        public async Task<Question?> UpdateAsync(int id, QuestionBusiness questionBusiness)
        {
            var question = await GetByIdAsync(id);
            if (question == null)
            {
                return null;
            }
            question.QuestionText = questionBusiness.QuestionText;
            question.AnswerA = questionBusiness.AnswerA;
            question.AnswerB = questionBusiness.AnswerB;
            question.AnswerC = questionBusiness.AnswerC;
            question.AnswerD = questionBusiness.AnswerD;
            question.AnswerCorrect = questionBusiness.AnswerCorrect;

            var questionEntity = await _unitOfWork.QuestionRepository.UpdateAsync(question);
            await _unitOfWork.SaveChangesAsync();
            return questionEntity;
        }
    }
}
