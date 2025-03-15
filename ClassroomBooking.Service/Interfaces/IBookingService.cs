using ClassroomBooking.Repository.Entities;

namespace ClassroomBooking.Service.Interfaces
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(int bookingId);
        Task CreateBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int bookingId);
        Task<List<Booking>> GetBookingsByStudentCodeAsync(string studentCode);
        Task<bool> UpdateBookingStatusAsync(int bookingId, string status);

    }
}
