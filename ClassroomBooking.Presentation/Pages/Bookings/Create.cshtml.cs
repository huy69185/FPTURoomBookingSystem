using ClassroomBooking.Repository.Entities;
using ClassroomBooking.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace ClassroomBooking.Presentation.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IClassroomService _classroomService;

        public CreateModel(IBookingService bookingService, IClassroomService classroomService)
        {
            _bookingService = bookingService;
            _classroomService = classroomService;
        }

        [BindProperty]
        public BookingCreateModel NewBooking { get; set; } = new();

        // Danh sách phòng (cho dropdown và layout sơ đồ)
        public List<Classrooms> ClassroomList { get; set; } = new();

        // Danh sách ID phòng đã được đặt trong khung giờ
        public List<int> BookedRoomIds { get; set; } = new();

        public string ErrorMessage { get; set; } = "";

        public async Task<IActionResult> OnGetAsync()
        {
            // Kiểm tra đăng nhập qua cookie (User.Identity)
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login");
            }

            // Lấy StudentCode từ User.Identity.Name
            NewBooking.StudentCode = User.Identity.Name;

            // Lấy danh sách phòng
            ClassroomList = await _classroomService.GetAllClassroomsAsync();

            // Gán thời gian mặc định, làm tròn đến phút (không có giây)
            var now = DateTime.Now;
            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            NewBooking.StartTime = now;
            NewBooking.EndTime = now.AddHours(1);

            // Nạp sơ đồ phòng theo khung giờ đã chọn
            await LoadRoomMapAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login");
            }
            NewBooking.StudentCode = User.Identity.Name;

            // Lấy lại danh sách phòng để cập nhật dropdown và layout
            ClassroomList = await _classroomService.GetAllClassroomsAsync();

            var action = Request.Form["action"].ToString();

            if (action == "refresh")
            {
                ModelState.Remove("NewBooking.ClassroomId");
                ModelState.Remove("NewBooking.Purpose");

                if (ModelState.IsValid)
                {
                    await LoadRoomMapAsync();
                }
                return Page();
            }

            if (!ModelState.IsValid)
            {
                await LoadRoomMapAsync();
                return Page();
            }

            try
            {
                var now = DateTime.Now;
                now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);

                // Kiểm tra nếu StartTime hoặc EndTime nằm trong quá khứ
                if (NewBooking.StartTime < now)
                {
                    ErrorMessage = "Start Time must be in the future!";
                    await LoadRoomMapAsync();
                    return Page();
                }

                if (NewBooking.EndTime <= NewBooking.StartTime)
                {
                    ErrorMessage = "End Time must be after Start Time!";
                    await LoadRoomMapAsync();
                    return Page();
                }

                var bookingEntity = new Booking
                {
                    StudentCode = NewBooking.StudentCode,
                    ClassroomId = NewBooking.ClassroomId,
                    StartTime = NewBooking.StartTime,
                    EndTime = NewBooking.EndTime,
                    Purpose = NewBooking.Purpose,
                    BookingStatus = "Pending",
                    CreatedDate = DateTime.Now
                };

                await _bookingService.CreateBookingAsync(bookingEntity);
                return RedirectToPage("/Bookings/Booking");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error creating booking: " + ex.Message;
                await LoadRoomMapAsync();
                return Page();
            }
        }

        private async Task LoadRoomMapAsync()
        {
            // Lấy tất cả booking
            var allBookings = await _bookingService.GetAllBookingsAsync();

            // Lọc các booking chồng chéo với khung giờ được nhập (StartTime, EndTime)
            var bookedRooms = allBookings
                .Where(b => b.EndTime > NewBooking.StartTime && b.StartTime < NewBooking.EndTime)
                .Select(b => b.ClassroomId)
                .Distinct()
                .ToList();

            BookedRoomIds = bookedRooms;
        }
    }
}
