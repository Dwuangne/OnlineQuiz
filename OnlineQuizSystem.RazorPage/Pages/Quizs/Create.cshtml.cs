using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineQuiz.Service.BusinessModel;
using OnlineQuiz.Service.Service.IService;
using OnlineQuizSystem.RazorPage.Models.RequestModel;

namespace OnlineQuizSystem.RazorPage.Pages.Quizs
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IQuizService _quizService;
        private readonly IMapper _mapper;

        public CreateModel(IQuizService quizService, IMapper mapper)
        {
            _quizService = quizService;
            _mapper = mapper;
            QuizRequest = new QuizRequest();
        }

        [BindProperty]
        public QuizRequest QuizRequest { get; set; } = default!;
        public IActionResult OnGet()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                QuizRequest.UserId = userId;
            }
            else
            {
                return RedirectToPage("Auth/Login");
            }


            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var quizBusiness = _mapper.Map<QuizBusiness>(QuizRequest);

            await _quizService.InsertAsync(quizBusiness);

            return RedirectToPage("./Index");
        }
    }
}
