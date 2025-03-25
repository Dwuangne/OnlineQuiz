using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineQuiz.Service.BusinessModel;
using OnlineQuiz.Service.Service.IService;
using OnlineQuizSystem.RazorPage.Models.RequestModel;
using OnlineQuizSystem.RazorPage.Models.ResponseModel;

namespace OnlineQuizSystem.RazorPage.Pages.Quizs
{
    public class EditModel : PageModel
    {
        private readonly IQuizService _quizService;
        private readonly IMapper _mapper;

        public EditModel(IQuizService quizService, IMapper mapper)
        {
            _quizService = quizService;
            _mapper = mapper;
        }

        [BindProperty]
        public QuizRequest QuizRequest { get; set; } = default!;

        public QuizResponse QuizResponse { get; set; } = default!; // Không c?n BindProperty vì ch? dùng ?? hi?n th?

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizEntity = await _quizService.GetByIdAsync((int)id);
            if (quizEntity == null)
            {
                return NotFound();
            }

            // Ánh x? t? QuizBusiness sang QuizResponse ?? hi?n th?
            QuizResponse = _mapper.Map<QuizResponse>(quizEntity);

            // Ánh x? t? QuizBusiness sang QuizRequest ?? binding vào form
            QuizRequest = _mapper.Map<QuizRequest>(quizEntity);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                // T?i l?i QuizResponse ?? hi?n th? n?u validation th?t b?i
                var quiz = await _quizService.GetByIdAsync(id);
                if (quiz == null)
                {
                    return NotFound();
                }
                QuizResponse = _mapper.Map<QuizResponse>(quiz);
                return Page();
            }

            var quizBusiness = _mapper.Map<QuizBusiness>(QuizRequest);
            var quizEntity = await _quizService.UpdateAsync(id, quizBusiness);
            if (quizEntity == null)
            {
                return NotFound();
            }

            return RedirectToPage("/Quizs/Index");
        }
    }
}