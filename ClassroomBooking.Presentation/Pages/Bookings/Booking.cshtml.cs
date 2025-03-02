using ClassroomBooking.Repository.Entities;
using ClassroomBooking.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ClassroomBooking.Presentation.Pages.Bookings
{
    public class BookingModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public BookingModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public List<Booking> BookingList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            // Lấy StudentCode từ session (được gán khi đăng nhập)
            var studentCode = HttpContext.Session.GetString("StudentCode");
            if (string.IsNullOrEmpty(studentCode))
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang Login
                return RedirectToPage("/Account/Login");
            }

            // Sử dụng phương thức của BookingService để lấy danh sách booking của sinh viên
            BookingList = await _bookingService.GetBookingsByStudentCodeAsync(studentCode);
            return Page();
        }
    }
}
