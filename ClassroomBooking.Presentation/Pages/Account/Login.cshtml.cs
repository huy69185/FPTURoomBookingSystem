using ClassroomBooking.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
                var student = await _studentService.LoginAsync(
                    LoginVM.StudentCode,
                    LoginVM.Password);

                // Đăng nhập thành công => Lưu session
                HttpContext.Session.SetString("StudentCode", student.StudentCode);

                return RedirectToPage("/Index");
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
