using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassroomBooking.Repository.Entities;

namespace ClassroomBooking.Service.Interfaces
{
    public interface IStudentService
    {
        Task<Students?> GetStudentAsync(string studentCode);
        Task RegisterStudentAsync(Students student);
        Task<Students> LoginAsync(string studentCode, string password);
        Task LogoutAsync();
    }
}
