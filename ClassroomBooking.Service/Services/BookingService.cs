using ClassroomBooking.Repository.Entities;
using ClassroomBooking.Repository.Interfaces;
using ClassroomBooking.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassroomBooking.Service.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;

        public BookingService(IBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _bookingRepo.GetAllAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int bookingId)
        {
            return await _bookingRepo.GetByIdAsync(bookingId);
        }

        public async Task CreateBookingAsync(Booking booking)
        {
            // Lấy tất cả booking cùng Classroom
            var allInSameRoom = await _bookingRepo.GetByClassroomAsync(booking.ClassroomId);

            // Kiểm tra trùng thời gian (overlap)
            bool isOverlap = allInSameRoom.Any(b =>
                // Điều kiện chồng chéo: booking cũ có EndTime > booking mới StartTime
                //                      VÀ booking cũ có StartTime < booking mới EndTime
                b.EndTime > booking.StartTime && b.StartTime < booking.EndTime
            );

            if (isOverlap)
            {
                throw new Exception("Phòng này đã có người đặt trong thời gian bạn chọn!");
            }

            // Nếu không trùng, cho phép tạo
            booking.CreatedDate = DateTime.Now;
            await _bookingRepo.CreateAsync(booking);
        }


        public async Task UpdateBookingAsync(Booking booking)
        {
            await _bookingRepo.UpdateAsync(booking);
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            await _bookingRepo.DeleteAsync(bookingId);
        }

        public async Task<List<Booking>> GetBookingsByStudentCodeAsync(string studentCode)
        {
            var allBookings = await _bookingRepo.GetAllAsync();
            return allBookings.Where(b => b.StudentCode == studentCode).ToList();
        }

        public async Task<bool> UpdateBookingStatusAsync(int bookingId, string status)
        {
            var result = await _bookingRepo.UpdateBookingStatusAsync(bookingId, status);
            return result;
        }
    }
}
