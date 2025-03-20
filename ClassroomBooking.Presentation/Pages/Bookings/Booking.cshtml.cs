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

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string StatusFilter { get; set; } = string.Empty;


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

            // Lọc theo từ khóa tìm kiếm (Classroom hoặc Purpose)
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                string lowerSearchTerm = SearchTerm.ToLower();
                BookingList = BookingList
                    .Where(b => b.ClassroomId.ToString().Contains(lowerSearchTerm) ||  // Chuyển int sang string
                                (!string.IsNullOrEmpty(b.Purpose) && b.Purpose.ToLower().Contains(lowerSearchTerm)))
                    .ToList();
            }

            //// Lọc theo trạng thái nếu người dùng đã chọn
            //if (!string.IsNullOrEmpty(StatusFilter) && StatusFilter != "All")
            //{
            //    BookingList = BookingList
            //        .Where(b => b.BookingStatus.Equals(StatusFilter, System.StringComparison.OrdinalIgnoreCase))
            //        .ToList();
            //}


            // Debug: Kiểm tra giá trị của StatusFilter
            Console.WriteLine($"StatusFilter: {StatusFilter}");

            // Lọc theo trạng thái nếu người dùng đã chọn
            if (!string.IsNullOrEmpty(StatusFilter) && StatusFilter != "All")
            {
                BookingList = BookingList
                    .Where(b => !string.IsNullOrEmpty(b.BookingStatus) &&
                                b.BookingStatus.Trim().Equals(StatusFilter.Trim(), System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Debug: Kiểm tra số lượng kết quả sau khi lọc
            Console.WriteLine($"Bookings after filtering: {BookingList.Count}");

            BookingList = BookingList;
            return Page();
        }
    }
}
