using ClassroomBooking.Repository.Entities;
using ClassroomBooking.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClassroomBooking.Presentation.Pages.Account
{
    public class SignupModel : PageModel
    {
        private readonly IStudentService _studentService;

        public SignupModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [BindProperty]
        public Students StudentModel { get; set; } = new();

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                // Gọi Service đăng ký
                await _studentService.RegisterStudentAsync(StudentModel);
                return RedirectToPage("/Account/Login");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }
}
