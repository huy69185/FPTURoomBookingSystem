using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassroomBooking.Repository.Entities;
using ClassroomBooking.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassroomBooking.Repository.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ClassroomBookingDbContext _db;

        public StudentRepository(ClassroomBookingDbContext db)
        {
            _db = db;
        }

        public async Task<Students?> GetByCodeAsync(string studentCode)
        {
            return await _db.Students
                .FirstOrDefaultAsync(s => s.StudentCode == studentCode);
        }

        public async Task CreateAsync(Students entity)
        {
            _db.Students.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Students entity)
        {
            _db.Students.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string studentCode)
        {
            var st = await GetByCodeAsync(studentCode);
            if (st != null)
            {
                _db.Students.Remove(st);
                await _db.SaveChangesAsync();
            }
        }
    }

}
