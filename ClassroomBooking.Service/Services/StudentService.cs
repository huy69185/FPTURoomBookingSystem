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

        public async Task<Students?> GetStudentAsync(string studentCode)
        {
            return await _studentRepo.GetByCodeAsync(studentCode);
        }

        public async Task RegisterStudentAsync(Students student)
        {
            var existed = await _studentRepo.GetByCodeAsync(student.StudentCode);
            if (existed != null)
                throw new Exception("StudentCode already exists!");

            // Tự động set Role = 2 (mặc định Học sinh)
            student.Role = 2;

            // Lưu student
            await _studentRepo.CreateAsync(student);
        }

        public async Task<Students> LoginAsync(string studentCode, string password)
        {
            var student = await _studentRepo.GetByCodeAsync(studentCode);
            if (student == null)
            {
                throw new Exception("Student not found!");
            }
            if (student.Password != password)
            {
                throw new Exception("Invalid password!");
            }

            // Đăng nhập thành công
            return student;
        }

        public Task LogoutAsync()
        {
            // Hiện tại không lưu token/phiên ở DB, nên để trống
            return Task.CompletedTask;
        }
    }
}
