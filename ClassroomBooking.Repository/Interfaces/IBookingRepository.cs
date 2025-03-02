using ClassroomBooking.Repository.Entities;

namespace ClassroomBooking.Repository.Interfaces
{
    public interface IBookingRepository
    {
        Task CreateAsync(Booking booking);
        Task<Booking?> GetByIdAsync(int bookingId);
        Task<List<Booking>> GetAllAsync();
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(int bookingId);
    }
}
