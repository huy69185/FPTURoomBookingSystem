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

    public class DeleteModel : PageModel
    {
        private readonly ClassroomBooking.Repository.Entities.ClassroomBookingDbContext _context;

        public DeleteModel(ClassroomBooking.Repository.Entities.ClassroomBookingDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classrooms = await _context.Classrooms.FindAsync(id);
            if (classrooms != null)
            {
                Classrooms = classrooms;
                _context.Classrooms.Remove(Classrooms);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ManageClassrooms");
        }
    }
}
