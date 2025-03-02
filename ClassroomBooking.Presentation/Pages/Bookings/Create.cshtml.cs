using ClassroomBooking.Repository.Entities;
using ClassroomBooking.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

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

        // Danh sách phòng để hiển thị trong dropdown
        public List<Classrooms> ClassroomList { get; set; } = new();

        public string ErrorMessage { get; set; } = "";

        public async Task<IActionResult> OnGetAsync()
        {
            // Kiểm tra đăng nhập: lấy StudentCode từ session
            var studentCode = HttpContext.Session.GetString("StudentCode");
            if (string.IsNullOrEmpty(studentCode))
            {
                return RedirectToPage("/Account/Login");
            }

            // Gán StudentCode tự động từ session (không cho user nhập)
            NewBooking.StudentCode = studentCode;

            // Lấy danh sách phòng
            ClassroomList = await _classroomService.GetAllClassroomsAsync();

            // Gán thời gian mặc định với giây bị loại bỏ (chỉ giờ và phút)
            var now = DateTime.Now;
            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            NewBooking.StartTime = now;
            NewBooking.EndTime = now.AddHours(1);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Kiểm tra đăng nhập
            var studentCode = HttpContext.Session.GetString("StudentCode");
            if (string.IsNullOrEmpty(studentCode))
            {
                return RedirectToPage("/Account/Login");
            }
            // Đảm bảo StudentCode từ session luôn được gán
            NewBooking.StudentCode = studentCode;

            if (!ModelState.IsValid)
            {
                ClassroomList = await _classroomService.GetAllClassroomsAsync();
                return Page();
            }

            try
            {
                if (NewBooking.EndTime <= NewBooking.StartTime)
                {
                    ErrorMessage = "End Time must be after Start Time!";
                    ClassroomList = await _classroomService.GetAllClassroomsAsync();
                    return Page();
                }

                // Chuyển BookingCreateModel sang entity Booking
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
                ClassroomList = await _classroomService.GetAllClassroomsAsync();
                return Page();
            }
        }
    }
}
