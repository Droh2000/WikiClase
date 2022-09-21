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
    public class DeleteModel : PageModel
    {
        private readonly WikiClase.Data.AplicationDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteModel(WikiClase.Data.AplicationDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id,string fname)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);

            fname = Path.Combine(_webHostEnvironment.WebRootPath, "Images", post.nombreImagen);
            FileInfo fi = new FileInfo(fname);
            if (fi.Exists)
            {
                System.IO.File.Delete(fname);
                fi.Delete();
            }

            if (post != null)
            {
                Post = post;
                _context.Posts.Remove(Post);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
