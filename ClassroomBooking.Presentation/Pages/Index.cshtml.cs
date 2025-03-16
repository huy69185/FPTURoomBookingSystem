using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClassroomBooking.Presentation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated) // Kiểm tra nếu đã đăng nhập
            {
                if (User.IsInRole("Admin"))
                {
                    return Redirect("/Admin/Index"); // Chuyển hướng Admin
                }
                else if (User.IsInRole("User"))
                {
                    return Redirect("/Index"); // Chuyển hướng User
                }
            }

            return Page(); // Nếu chưa đăng nhập, hiển thị trang Index mặc định
        }
    }
}
