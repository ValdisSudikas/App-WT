using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Сосновский.UI.Data;
using Air.Domain.Entities;

namespace Сосновский.UI.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Airplane airplane { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Airplanes == null)
            {
                return NotFound();
            }

            var dish = await _context.Airplanes.FirstOrDefaultAsync(m => m.Id == id);

            if (dish == null)
            {
                return NotFound();
            }
            else
            {
                airplane = airplane;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Airplanes == null)
            {
                return NotFound();
            }
            var dish = await _context.Airplanes.FindAsync(id);

            if (dish != null)
            {
                airplane = airplane;
                _context.Airplanes.Remove(airplane);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
