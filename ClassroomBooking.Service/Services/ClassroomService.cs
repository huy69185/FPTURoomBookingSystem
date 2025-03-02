using ClassroomBooking.Repository.Entities;
using ClassroomBooking.Repository.Interfaces; // Nếu bạn có IClassroomRepository
using ClassroomBooking.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassroomBooking.Service.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly IClassroomRepository _classroomRepo;

        public ClassroomService(IClassroomRepository classroomRepo)
        {
            _classroomRepo = classroomRepo;
        }

        public async Task<List<Classrooms>> GetAllClassroomsAsync()
        {
            // Gọi repository để lấy danh sách phòng
            return await _classroomRepo.GetAllAsync();
        }
    }
}
