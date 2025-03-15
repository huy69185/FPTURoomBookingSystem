using ClassroomBooking.Repository.Entities;
using ClassroomBooking.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassroomBooking.Repository.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ClassroomBookingDbContext _db;
        // Giả sử DbContext bạn scaffold tên "ClassroomBookingDbContext"

        public BookingRepository(ClassroomBookingDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Booking booking)
        {
            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();
        }

        public async Task<Booking?> GetByIdAsync(int bookingId)
        {
            // Include navigation (Students, Classrooms) nếu bạn muốn lấy thêm thông tin
            return await _db.Bookings
                .Include(b => b.StudentCodeNavigation)
                .Include(b => b.Classroom)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _db.Bookings
                .Include(b => b.StudentCodeNavigation)
                .Include(b => b.Classroom)
                .OrderBy(b => b.BookingId)  // Sắp xếp tăng dần theo BookingId
                .ToListAsync();
        }


        public async Task UpdateAsync(Booking booking)
        {
            _db.Bookings.Update(booking);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int bookingId)
        {
            var entity = await GetByIdAsync(bookingId);
            if (entity != null)
            {
                _db.Bookings.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }
        public async Task<List<Booking>> GetByClassroomAsync(int classroomId)
        {
            return await _db.Bookings
                .Where(b => b.ClassroomId == classroomId)
                .ToListAsync();
        }

        public async Task<bool> UpdateBookingStatusAsync(int bookingId, string status)
        {
            var booking = await GetByIdAsync(bookingId);

            if (booking == null) return false;

            booking.BookingStatus = status;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
