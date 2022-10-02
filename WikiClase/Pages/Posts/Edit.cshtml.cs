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
        private readonly WikiClase.Data.AplicationDBContext _context; // Este leera en la BASE de DAtos
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(WikiClase.Data.AplicationDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            // inyeccion de dependencia
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // con esto nos movemos entre la vista y el controlador
        [BindProperty]
        public Post Post { get; set; } = default!;

        // async lleva siempre un IACtionResult (Podemos mandar cualquier cosa 'return')
        public async Task<IActionResult> OnGetAsync(int? id)//? campo opcional
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }
            // Entramos a la tabla y no de el primer elemento (Es como un SELECT donde ese where se cumpla)
            var post =  await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);// m es una variable
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
        public async Task<IActionResult> OnPostAsync(IFormFile uploadfiles,int id) // Este es el que manda a la base de datos
        {
            // Este nos afecta si el dato es obligatorio y no mandamos nada se activa este if
            // o mandamos NULL
            /*if (!ModelState.IsValid)
            {
                //return Page();
                return BadRequest(ModelState);
            }*/

            // modifica el post con el blindproperti
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
