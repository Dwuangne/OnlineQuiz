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
    public class QuizService : IQuizService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuestionService _questionService;

        public QuizService(IUnitOfWork unitOfWork, IQuestionService questionService)
        {
            _unitOfWork = unitOfWork;
            _questionService = questionService;
        }

        public async Task<Quiz> DeleteAsync(int id)
        {
            var quiz = await GetByIdAsync(id);
            if (quiz == null)
            {
                return null;
            }
            quiz.IsDeleted = true;
            var quizEntity = await _unitOfWork.QuizRepository.UpdateAsync(quiz);
            await _unitOfWork.SaveChangesAsync();

            return quizEntity;
        }

        public async Task<(IEnumerable<Quiz>, int PageIndex, int TotalPages, int TotalRecords)> GetAsync(Expression<Func<Quiz, bool>> filter = null, Func<IQueryable<Quiz>, IOrderedQueryable<Quiz>> orderBy = null, string includeProperties = "", int? pageIndex = null, int? pageSize = null)
        {
            return await _unitOfWork.QuizRepository.GetAsync(filter: s => s.IsDeleted == false, orderBy, includeProperties, pageIndex, pageSize);
        }

        public async Task<Quiz?> GetByIdAsync(int id)
        {
            var entities = await _unitOfWork.QuizRepository.GetAsync(
               filter: q => q.QuizId == id,
               orderBy: null,
               includeProperties: "Questions",
               pageIndex: 1,
               pageSize: 1
           );
            var entity = entities.Item1.FirstOrDefault();
            if (entity != null)
            {
                entity.Questions = entity.Questions.Where(q => q.IsDeleted == false).ToList();
            }
            return entity;
        }

        public async Task<Quiz> InsertAsync(QuizBusiness quizBusiness)
        {
            var quizEntity = new Quiz
            {
                UserId = quizBusiness.UserId,
                Title = quizBusiness.Title,
                Description = quizBusiness.Description,
                CreatedAt = DateTime.Now,
            };
            quizEntity = await _unitOfWork.QuizRepository.InsertAsync(quizEntity);
            await _unitOfWork.SaveChangesAsync();

            foreach (var question in quizBusiness.Questions)
            {
                var questionEntity = await _questionService.InsertAsync(quizEntity.QuizId, question);
            }
            return quizEntity;
        }

        public async Task<Quiz?> UpdateAsync(int id, QuizBusiness quizBusiness)
        {
            var entities = await _unitOfWork.QuizRepository.GetAsync(
               filter: q => q.QuizId == id,
               orderBy: null,
               includeProperties: "",
               pageIndex: 1,
               pageSize: 1
           );
            var quiz = entities.Item1.FirstOrDefault();
            if (quiz == null)
            {
                return null;
            }
            quiz.Title = quizBusiness.Title;
            quiz.Description = quizBusiness.Description;
            var quizEntity = await _unitOfWork.QuizRepository.UpdateAsync(quiz);
            await _unitOfWork.SaveChangesAsync();

            return quizEntity;
        }
    }
}
