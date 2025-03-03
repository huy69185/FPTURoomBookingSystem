using ClassroomBooking.Repository.Entities;
using ClassroomBooking.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassroomBooking.Repository.Repositories
{
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly ClassroomBookingDbContext _db;

        public ClassroomRepository(ClassroomBookingDbContext db)
        {
            _db = db;
        }

        public async Task<List<Classrooms>> GetAllAsync()
        {
            return await _db.Classrooms.ToListAsync();
        }
    }
}
