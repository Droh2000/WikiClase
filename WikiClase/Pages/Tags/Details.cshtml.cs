using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WikiClase.Data;
using WikiClase.Models;

namespace WikiClase.Pages.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly WikiClase.Data.AplicationDBContext _context;

        public DetailsModel(WikiClase.Data.AplicationDBContext context)
        {
            _context = context;
        }

      public Tag Tag { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Tags == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }
            else 
            {
                Tag = tag;
            }
            if (_context.Tags != null)
            {
                IList<Tag> Tag = default!;
                Tag = await _context.Tags.Include(t => t.Categoria).ToListAsync();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "nombreCategoria");
            return Page();
        }
    }
}
