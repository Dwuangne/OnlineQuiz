using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineQuiz.Service.Service.IService;
using OnlineQuizSystem.RazorPage.Models.RequestModel;
using System.Security.Claims;
using OnlineQuiz.Service.Service;

namespace OnlineQuizSystem.RazorPage.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;

        public LoginModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public LoginRequest LoginRequest { get; set; } = new();
        public string ErrorMessage { get; set; } = string.Empty;
        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity.IsAuthenticated) // Kiểm tra đăng nhập thông qua Cookies
            {
                return RedirectToPage("/Quizs/Index");

            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please provide all the required fields";
                return Page();
            }

            var user = await _authService.LoginAsync(LoginRequest.EmailAddress, LoginRequest.Password);
            if (user == null)
            {
                ErrorMessage = "Invalid email or password.";
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Lưu cookies
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = true, // Giữ đăng nhập ngay cả khi đóng trình duyệt
                    ExpiresUtc = DateTime.UtcNow.AddDays(7) // Cookies hết hạn sau 7 ngày
                });

            return RedirectToPage("/Quizs/Index"); // Sửa đường dẫn đúng thư mục
        }
    }
}
