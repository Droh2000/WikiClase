using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WikiClase.Data;
using WikiClase.Models;

namespace WikiClase.Pages.Posts
{
    public class EditModel : PageModel
    {
        private readonly WikiClase.Data.AplicationDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(WikiClase.Data.AplicationDBContext context, IWebHostEnvironment webHostEnvironment)
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

            var post =  await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            Post = post;
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "nombreCategoria");
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "subCategoria");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile uploadfiles,int id)
        {
            /*if (!ModelState.IsValid)
            {
                //return Page();
                return BadRequest(ModelState);
            }*/

            _context.Attach(Post).State = EntityState.Modified;

            try
            {
                if (uploadfiles != null)
                {
                    var imgid = await _context.Posts.FindAsync(id);
                    _context.Posts.Remove(imgid);
                    string fname = Path.Combine(_webHostEnvironment.WebRootPath, "Images", imgid.nombreImagen);
                    FileInfo fi = new FileInfo(fname);
                    if (fi.Exists)
                    {
                        System.IO.File.Delete(fname);
                        fi.Delete();
                    }
                    string imgext = Path.GetExtension(uploadfiles.FileName);
                    if (imgext == ".jpg" || imgext == ".gif" || imgext == ".png")
                    {
                        var imgsave = Path.Combine(_webHostEnvironment.WebRootPath, "Images", uploadfiles.FileName);
                        var stream = new FileStream(imgsave, FileMode.Create);
                        await uploadfiles.CopyToAsync(stream);

                        stream.Close();
                        Post.Id = id;
                        Post.nombreImagen = uploadfiles.FileName;
                        Post.rutaImagen = imgsave;
                        _context.Update(Post);
                        await _context.SaveChangesAsync();
                    }
                }
                else {
                    var fileName = Path.GetFileName(Post.nombreImagen);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                    Post.rutaImagen = filePath;
                    _context.Update(Post);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(Post.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PostExists(int id)
        {
          return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
