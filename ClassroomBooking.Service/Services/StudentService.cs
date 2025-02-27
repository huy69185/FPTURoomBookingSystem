using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassroomBooking.Repository.Entities;
using ClassroomBooking.Repository.Interfaces;
using ClassroomBooking.Service.Interfaces;

namespace ClassroomBooking.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;

        public StudentService(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        /// <summary>
        /// Lấy thông tin Student theo StudentCode
        /// </summary>
        public async Task<Students?> GetStudentAsync(string studentCode)
        {
            return await _studentRepo.GetByCodeAsync(studentCode);
        }

        /// <summary>
        /// Đăng ký student mới
        /// - Kiểm tra StudentCode đã tồn tại chưa
        /// - Lưu student vào DB
        /// </summary>
        public async Task RegisterStudentAsync(Students student)
        {
            // Kiểm tra trùng mã
            var existed = await _studentRepo.GetByCodeAsync(student.StudentCode);
            if (existed != null)
                throw new Exception("StudentCode already exists!");

            // Ở đây, bạn có thể hash Password nếu muốn.
            //  student.Password = HashPassword(student.Password);

            // Thêm mới
            await _studentRepo.CreateAsync(student);
        }

        /// <summary>
        /// Đăng nhập bằng StudentCode + Password 
        /// Throw Exception nếu không hợp lệ
        /// </summary>
        public async Task<Students> LoginAsync(string studentCode, string password)
        {
            var student = await _studentRepo.GetByCodeAsync(studentCode);
            if (student == null)
            {
                throw new Exception("Student not found!");
            }

            // Ở đây, nếu bạn lưu hashed password, 
            //  bạn cần so sánh Hash của password nhập vào với student.PasswordHash trong DB.
            if (student.Password != password)
            {
                throw new Exception("Invalid password!");
            }

            // Đăng nhập thành công => Trả về entity
            return student;
        }

        /// <summary>
        /// Đăng xuất (nếu Service cần xóa token/refresh token trong DB, 
        /// thì xử lý, còn không thì bỏ trống)
        /// </summary>
        public Task LogoutAsync()
        {
            // Ví dụ: nếu bạn không lưu Session/Token trong DB thì không cần làm gì.
            return Task.CompletedTask;
        }
    }
}
