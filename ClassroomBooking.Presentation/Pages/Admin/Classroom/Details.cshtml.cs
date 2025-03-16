using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClassroomBooking.Repository.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ClassroomBooking.Presentation.Pages.Admin.Classroom
{
    [Authorize(Roles = "Admin")]

    public class DetailsModel : PageModel
    {
        private readonly ClassroomBooking.Repository.Entities.ClassroomBookingDbContext _context;

        public DetailsModel(ClassroomBooking.Repository.Entities.ClassroomBookingDbContext context)
        {
            _context = context;
        }

        public Classrooms Classrooms { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classrooms = await _context.Classrooms.FirstOrDefaultAsync(m => m.ClassroomId == id);
            if (classrooms == null)
            {
                return NotFound();
            }
            else
            {
                Classrooms = classrooms;
            }
            return Page();
        }
    }
}
