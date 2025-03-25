using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineQuiz.Service.Service.IService;
using OnlineQuizSystem.RazorPage.Models.ResponseModel;

namespace OnlineQuizSystem.RazorPage.Pages.Quizs
{
    public class DeleteModel : PageModel
    {
        private readonly IQuizService _quizService;
        private readonly IMapper _mapper;

        public DeleteModel(IQuizService quizService, IMapper mapper)
        {
            _quizService = quizService;
            _mapper = mapper;
        }

        [BindProperty]
        public QuizResponse QuizResponse { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var entity = await _quizService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            QuizResponse = _mapper.Map<QuizResponse>(entity);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var quiz = await _quizService.DeleteAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
