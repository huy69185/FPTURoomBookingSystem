using ClassroomBooking.Repository.Entities;

namespace ClassroomBooking.Service.Interfaces
{
    public interface IClassroomService
    {
        Task<List<Classrooms>> GetAllClassroomsAsync();
        // Bạn có thể bổ sung thêm các phương thức khác (GetById, Create, ...)
    }
}
