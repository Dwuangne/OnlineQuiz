using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Repository.UnitOfWork;
using OnlineQuiz.Service.Service.IService;
using OnlineQuizSystem.RazorPage.Models.ResponseModel;
using System.Linq.Expressions;

namespace OnlineQuizSystem.RazorPage.Pages.Quizs
{
    public class IndexModel : PageModel
    {
        public IQuizService _quizService { get; }
        private readonly IMapper _mapper;
        public IndexModel(IQuizService quizService, IMapper mapper)
        {
            _quizService = quizService;
            _mapper = mapper;
        }

        public int pageIndex = 1;
        public int totalPages = 0;
        private readonly int pageSize = 5;

        public IList<QuizResponse> QuizResponses { get; set; } = default!;


        public async Task OnGet(int? pageIndex)
        {
            Expression<Func<Quiz, bool>> filterExpression = null;
            Func<IQueryable<Quiz>, IOrderedQueryable<Quiz>> orderByFunc = null;
            var entities = await _quizService.GetAsync(filterExpression, orderByFunc, "", pageIndex, pageSize);
            var response = _mapper.Map<IList<QuizResponse>>(entities.Item1);

            this.totalPages = entities.TotalPages;
            this.pageIndex = entities.PageIndex;
            this.QuizResponses = response;
        }
    }
}
