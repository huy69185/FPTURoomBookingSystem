using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClassroomBooking.Repository.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ClassroomBooking.Presentation.Pages.Admin.Classroom
{
    [Authorize(Roles = "Admin")]

    public class CreateModel : PageModel
    {
        private readonly ClassroomBooking.Repository.Entities.ClassroomBookingDbContext _context;

        public CreateModel(ClassroomBooking.Repository.Entities.ClassroomBookingDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Classrooms Classrooms { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Classrooms.Add(Classrooms);
            await _context.SaveChangesAsync();

            return RedirectToPage("./ManageClassrooms");
        }
    }
}
