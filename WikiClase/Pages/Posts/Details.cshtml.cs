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

namespace WikiClase.Pages.Posts
{
    public class DetailsModel : PageModel
    {
        private readonly WikiClase.Data.AplicationDBContext _context;

        public DetailsModel(WikiClase.Data.AplicationDBContext context)
        {
            _context = context;
        }

      public Post Post { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            else 
            {
                Post = post;
            }
            if (_context.Posts != null)
            {
                IList<Post> Post = default!;
                Post = await _context.Posts.Include(t => t.Categoria).ToListAsync();
                Post = await _context.Posts.Include(x => x.Tag).ToListAsync();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "nombreCategoria");
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "subCategoria");
            return Page();
        }
    }
}
