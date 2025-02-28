using ClassroomBooking.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ClassroomBooking.Presentation.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IStudentService _studentService;

        public LoginModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [BindProperty]
        public LoginViewModel LoginVM { get; set; } = new();

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                var student = await _studentService.LoginAsync(LoginVM.StudentCode, LoginVM.Password);

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, student.StudentCode),
            new Claim(ClaimTypes.Role, student.Role == 1 ? "Admin" : "Student") // Xác định quyền
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Duy trì phiên đăng nhập
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return student.Role == 1 ? RedirectToPage("/Admin/Index") : RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }

  }

    public class LoginViewModel
    {
        public string? StudentCode { get; set; }
        public string? Password { get; set; }
    }
}
