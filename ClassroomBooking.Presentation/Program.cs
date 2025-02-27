using ClassroomBooking.Repository.Repositories;  // Thư viện Repository
using ClassroomBooking.Service.Services;        // Thư viện Service
using ClassroomBooking.Presentation.Hubs;       // Hub cho SignalR (nếu có)
using Microsoft.EntityFrameworkCore;
using ClassroomBooking.Repository.Interfaces;
using ClassroomBooking.Service.Interfaces;
using ClassroomBooking.Repository.Entities;

var builder = WebApplication.CreateBuilder(args);

// 1) Khai báo chuỗi kết nối SQL Server
var connString = "Server=Huy\\Huy;Database=ClassroomBookingDB;User Id=sa;Password=12345";
// Hoặc ví dụ:
// var connString = "Server=.;Database=ClassroomBookingDB;Trusted_Connection=True;";

// 2) Thêm DbContext (EF Core)
builder.Services.AddDbContext<ClassroomBookingDbContext>(options =>
    options.UseSqlServer(connString));

// 3) Đăng ký Repository (DI)
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
// builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();
// builder.Services.AddScoped<IBookingRepository, BookingRepository>();
// ... tuỳ bạn đã tạo repository nào ...

// 4) Đăng ký Service (DI)
builder.Services.AddScoped<IStudentService, StudentService>();
// builder.Services.AddScoped<IClassroomService, ClassroomService>();
// builder.Services.AddScoped<IBookingService, BookingService>();
// ... tuỳ bạn đã tạo service nào ...

// 5) Thêm SignalR
builder.Services.AddSignalR();

// 6) Nếu bạn cần session để lưu thông tin Login, kích hoạt session
builder.Services.AddSession();

// 7) Thêm Razor Pages
builder.Services.AddRazorPages();

// Build app
var app = builder.Build();

// Middlewares
// Production error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Sử dụng HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();

// Routing
app.UseRouting();

// Dùng session (nếu đã đăng ký)
app.UseSession();

// Authorization (nếu cần, Identity v.v.)
app.UseAuthorization();

// Map Razor Pages
app.MapRazorPages();

// Map SignalR Hub nếu bạn có
// Ví dụ: app.MapHub<BookingHub>("/bookingHub");
// Khai báo route cho BookingHub
app.MapHub<BookingHub>("/bookingHub");
app.Run();
