using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineQuiz.Service.BusinessModel;
using OnlineQuiz.Service.Service;
using OnlineQuiz.Service.Service.IService;
using OnlineQuizSystem.RazorPage.Models.RequestModel;

namespace OnlineQuizSystem.RazorPage.Pages.Questions
{
    public class CreateModel : PageModel
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public CreateModel(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }
        [BindProperty]
        public QuestionRequest QuestionRequest { get; set; } = default!;
        public IActionResult OnGet(int quizId)
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int quizId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var questionBusiness = _mapper.Map<QuestionBusiness>(QuestionRequest);

            var newQuestion = await _questionService.InsertAsync(quizId, questionBusiness);

            return RedirectToPage("/Quizs/Edit", new { id = newQuestion.QuizId });

        }
    }
}
