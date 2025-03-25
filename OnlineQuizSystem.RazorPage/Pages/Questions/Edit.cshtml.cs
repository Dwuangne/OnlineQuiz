using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Service.BusinessModel;
using OnlineQuiz.Service.Service;
using OnlineQuiz.Service.Service.IService;
using OnlineQuizSystem.RazorPage.Models.RequestModel;

namespace OnlineQuizSystem.RazorPage.Pages.Questions
{
    public class EditModel : PageModel
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public EditModel(IQuestionService questionService, IMapper mapper)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var questionBusiness = _mapper.Map<QuestionBusiness>(QuestionRequest);
            var questionEntity = await _questionService.UpdateAsync(id, questionBusiness);
            if (questionEntity == null)
            {
                return NotFound();
            }
            return RedirectToPage("/Quizs/Edit", new { id = questionEntity.QuizId });
        }
    }
}
