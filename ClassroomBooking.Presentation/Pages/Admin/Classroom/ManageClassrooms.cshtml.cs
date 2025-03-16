using ClassroomBooking.Repository.Entities;
using ClassroomBooking.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClassroomBooking.Presentation.Pages.Admin.Classroom
{
    [Authorize(Roles = "Admin")]
    public class ManageClassroomsModel : PageModel
    {
        private readonly IClassroomService _classroomService;

        public ManageClassroomsModel(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        public List<Classrooms> ClassroomList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            ClassroomList = await _classroomService.GetAllClassroomsAsync();
            return Page();
        }
    }
}
