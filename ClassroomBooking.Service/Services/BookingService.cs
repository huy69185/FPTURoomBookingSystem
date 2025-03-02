using ClassroomBooking.Repository.Entities;
using ClassroomBooking.Repository.Interfaces;
using ClassroomBooking.Service.Interfaces;

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
            // Gán mặc định CreatedDate
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
    }
}
