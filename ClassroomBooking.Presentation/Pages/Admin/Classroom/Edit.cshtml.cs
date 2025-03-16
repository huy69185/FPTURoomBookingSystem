using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassroomBooking.Repository.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ClassroomBooking.Presentation.Pages.Admin.Classroom
{
    [Authorize(Roles = "Admin")]

    public class EditModel : PageModel
    {
        private readonly ClassroomBooking.Repository.Entities.ClassroomBookingDbContext _context;

        public EditModel(ClassroomBooking.Repository.Entities.ClassroomBookingDbContext context)
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

            var classrooms =  await _context.Classrooms.FirstOrDefaultAsync(m => m.ClassroomId == id);
            if (classrooms == null)
            {
                return NotFound();
            }
            Classrooms = classrooms;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Classrooms).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassroomsExists(Classrooms.ClassroomId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./ManageClassrooms");
        }

        private bool ClassroomsExists(int id)
        {
            return _context.Classrooms.Any(e => e.ClassroomId == id);
        }
    }
}
