using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineQuiz.Service.Service;
using OnlineQuiz.Service.Service.IService;
using OnlineQuizSystem.RazorPage.Models.RequestModel;

namespace OnlineQuizSystem.RazorPage.Pages.Questions
{
    public class DeleteModel : PageModel
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public DeleteModel(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        [BindProperty]
        public QuestionRequest QuestionRequest { get; set; } = new QuestionRequest();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionBusiness = await _questionService.GetByIdAsync((int)id);
            if (questionBusiness == null)
            {
                return NotFound();
            }
            QuestionRequest = _mapper.Map<QuestionRequest>(questionBusiness);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var question = await _questionService.GetByIdAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            var check = await _questionService.DeleteAsync(id);

            return RedirectToPage("/Quizs/Edit", new { id = question.QuizId });
        }
    }
}
