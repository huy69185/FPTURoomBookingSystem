using ClassroomBooking.Repository.Entities;
using ClassroomBooking.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClassroomBooking.Presentation.Pages.Admin
{
    public class AdminBookingModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public AdminBookingModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public List<Booking> BookingList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.IsInRole("Admin")) // Kiểm tra quyền admin
            {
                return RedirectToPage("/Account/Login");
            }

            // Lấy tất cả booking (Admin xem toàn bộ)
            BookingList = await _bookingService.GetAllBookingsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int bookingId, string bookingstatus)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            var success = await _bookingService.UpdateBookingStatusAsync(bookingId, bookingstatus);
            if (!success)
            {
                ModelState.AddModelError("", "Update Failed!");
            }

            return RedirectToPage();
        }
        
    }
}
