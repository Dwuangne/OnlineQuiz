using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.Service.BusinessModel;
using OnlineQuiz.Service.Service.IService;

namespace OnlineQuizSystem.RazorPage.Pages.Room {
public class CreateModel : PageModel
{
    private readonly IRoomService _roomService;
    private readonly IQuizService _quizService;
    private readonly IAuthService _authService;

    public CreateModel(IRoomService roomService, IQuizService quizService, IAuthService authService)
    {
        _roomService = roomService;
        _quizService = quizService;
        _authService = authService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var users = await _authService.GetUsersAsync();
        ViewData["HostId"] = new SelectList(users, "UserId", "Email");
        var quizResult = await _quizService.GetAsync();
        ViewData["QuizId"] = new SelectList(quizResult.Item1, "QuizId", "Title");
        return Page();
    }

        [BindProperty]
        public RoomBusiness Room { get; set; } = new RoomBusiness();

        public async Task<IActionResult> OnPostAsync()
    {
            // Sinh RoomCode nếu trống
            if (string.IsNullOrWhiteSpace(Room.RoomCode))
            {
                Room.RoomCode = Guid.NewGuid().ToString("N").Substring(0, 7);
            }
            ModelState.Remove("Room.RoomCode");
            if (!ModelState.IsValid)
        {
            var users = await _authService.GetUsersAsync();
            ViewData["HostId"] = new SelectList(users, "UserId", "Email");
            var quizResult = await _quizService.GetAsync();
            ViewData["QuizId"] = new SelectList(quizResult.Item1, "QuizId", "Title");
            return Page();
        }
            

            await _roomService.InsertAsync(Room);

        return RedirectToPage("./Index");
    }
}
}