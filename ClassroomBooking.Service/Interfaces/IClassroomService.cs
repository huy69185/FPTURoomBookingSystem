using ClassroomBooking.Repository.Entities;

namespace ClassroomBooking.Service.Interfaces
{
    public interface IClassroomService
    {
        Task<List<Classrooms>> GetAllClassroomsAsync();
        // Bạn có thể bổ sung thêm các phương thức khác (GetById, Create, ...)
        Task<Classrooms?> GetClassroomByIdAsync(int classroomId);
        Task CreateClassroomAsync(Classrooms classroomId);
        Task UpdateClassroomAsync(Classrooms classroomId);
        Task DeleteClassroomAsync(int classroomId);




    }
}
