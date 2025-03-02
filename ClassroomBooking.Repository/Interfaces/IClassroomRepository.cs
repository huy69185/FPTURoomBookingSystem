using ClassroomBooking.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomBooking.Repository.Interfaces
{
    public interface IClassroomRepository
    {
        Task<List<Classrooms>> GetAllAsync();
        // ...
    }
}
