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
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Airplane airplane { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Airplanes == null)
            {
                return NotFound();
            }

            var airplane = await _context.Airplanes.FirstOrDefaultAsync(m => m.Id == id);
            if (airplane == null)
            {
                return NotFound();
            }
            else
            {
                airplane = airplane;
            }
            return Page();
        }
    }
}