using ClassroomBooking.Repository.Entities;
using ClassroomBooking.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login");
            }

            // Lấy StudentCode từ User.Identity.Name
            var studentCode = User.Identity.Name;

            // Dùng service để lấy danh sách booking của sinh viên
            BookingList = await _bookingService.GetBookingsByStudentCodeAsync(studentCode);
            return Page();
        }
    }
}
