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

        public async Task CreateClassroomAsync(Classrooms classroomId)
        {
            await _classroomRepo.CreateClassroomAsync(classroomId);
        }

        public async Task DeleteClassroomAsync(int classroomId)
        {
            await _classroomRepo.DeleteClassroomAsync(classroomId);
        }

        public async Task<List<Classrooms>> GetAllClassroomsAsync()
        {
            // Gọi repository để lấy danh sách phòng
            return await _classroomRepo.GetAllAsync();
        }

        public async Task<Classrooms?> GetClassroomByIdAsync(int classroomId)
        {
            if (classroomId < 0)
            {
                return null; 
            }

            return await _classroomRepo.GetClassroomByIdAsync(classroomId);
        }



        public async Task UpdateClassroomAsync(Classrooms classroomId)
        {
            await _classroomRepo.UpdateClassroomAsync(classroomId);
        }
    }
}
