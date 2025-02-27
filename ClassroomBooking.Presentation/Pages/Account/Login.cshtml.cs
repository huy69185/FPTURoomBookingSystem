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

                // Lưu session
                HttpContext.Session.SetString("StudentCode", student.StudentCode);
                HttpContext.Session.SetInt32("Role", student.Role);

                // Kiểm tra Role
                if (student.Role == 1)
                {
                    // Role = 1 => Admin
                    return RedirectToPage("/Admin/Index"); // ví dụ trang Admin
                }
                else
                {
                    // Role = 2 => Học sinh
                    return RedirectToPage("/Index");
                }
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
