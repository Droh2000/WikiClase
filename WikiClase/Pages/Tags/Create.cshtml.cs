using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WikiClase.Data;
using WikiClase.Models;

namespace WikiClase.Pages.Tags
{
    public class CreateModel : PageModel
    {
        private readonly WikiClase.Data.AplicationDBContext _context;

        public CreateModel(WikiClase.Data.AplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "nombreCategoria");
            return Page();
        }

        [BindProperty]
        public Tag Tag { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Tags == null || Tag == null)
            {
                //return Page();
                return BadRequest(ModelState);
            }

            _context.Tags.Add(Tag);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
