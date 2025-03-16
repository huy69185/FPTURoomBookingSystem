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

        public Task CreateClassroomAsync(Classrooms classroom)
        {
            _db.Classrooms.Add(classroom);
            return _db.SaveChangesAsync();
        }

        public async Task DeleteClassroomAsync(int classroom)
        {
            var entity = await GetClassroomByIdAsync(classroom);
            if (entity != null)
            {
                _db.Classrooms.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Classrooms>> GetAllAsync()
        {
            return await _db.Classrooms.ToListAsync();
        }

        public async Task<Classrooms?> GetClassroomByIdAsync(int classroomId)
        {
            return await _db.Classrooms
                          .FirstOrDefaultAsync(b => b.ClassroomId == classroomId);
            
        }

        public Task UpdateClassroomAsync(Classrooms classroom)
        {
            _db.Classrooms.Update(classroom);
            return _db.SaveChangesAsync();
        }
    }
}
