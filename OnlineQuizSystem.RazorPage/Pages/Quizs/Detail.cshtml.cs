using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineQuiz.Service.Service.IService;
using OnlineQuizSystem.RazorPage.Models.ResponseModel;

namespace OnlineQuizSystem.RazorPage.Pages.Quizs
{
    public class DetailModel : PageModel
    {
        private readonly IQuizService _quizService;
        private readonly IMapper _mapper;

        public DetailModel(IQuizService quizService, IMapper mapper)
        {
            _quizService = quizService;
            _mapper = mapper;
        }

        public QuizResponse QuizResponse { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _quizService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                QuizResponse = _mapper.Map<QuizResponse>(entity);
            }
            return Page();
        }
    }
}
