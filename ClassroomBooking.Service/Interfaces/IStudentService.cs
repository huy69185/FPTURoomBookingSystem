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
        /// <summary>
        /// Lấy thông tin Student theo StudentCode
        /// </summary>
        Task<Students?> GetStudentAsync(string studentCode);

        /// <summary>
        /// Đăng ký (thêm mới) 1 Student 
        /// (có thể bao gồm cả thiết lập mật khẩu)
        /// </summary>
        Task RegisterStudentAsync(Students student);

        /// <summary>
        /// Đăng nhập bằng StudentCode + Password 
        /// Trả về đối tượng Students nếu hợp lệ, 
        /// hoặc throw Exception nếu thất bại
        /// </summary>
        Task<Students> LoginAsync(string studentCode, string password);

        /// <summary>
        /// Đăng xuất (thường là no-op ở Service 
        /// nếu không quản lý session/token phía DB)
        /// </summary>
        Task LogoutAsync();
    }
}
