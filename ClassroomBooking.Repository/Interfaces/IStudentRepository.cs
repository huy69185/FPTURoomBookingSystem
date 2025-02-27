using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassroomBooking.Repository.Entities;

namespace ClassroomBooking.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Task<Students?> GetByCodeAsync(string studentCode);
        Task CreateAsync(Students entity);
        Task UpdateAsync(Students entity);
        Task DeleteAsync(string studentCode);
    }
}
