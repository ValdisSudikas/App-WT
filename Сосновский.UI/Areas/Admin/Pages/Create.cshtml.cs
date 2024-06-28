using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Сосновский.UI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using Air.Domain.Entities;
using Сосновский.UI.Services;

namespace Сосновский.UI.Areas.Admin.Pages
{
    public class CreateModel(ICategoryService categoryService, IProductService productService) : PageModel
    {
     
            public async Task<IActionResult> OnGet()
            {
                var categoryListData = await categoryService.GetCategoryListAsync();
                ViewData["CategoryId"] = new SelectList(categoryListData.Data, "Id",
                "GroupName");
                return Page();
            }
            [BindProperty]
            public Airplane airplane { get; set; } = default!;
            [BindProperty]
            public IFormFile? Image { get; set; }

            public async Task<IActionResult> OnPostAsync()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                await productService.CreateProductAsync(airplane, Image);
                return RedirectToPage("./Index");
            }
        }
    }
