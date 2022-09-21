using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WikiClase.Data;
using WikiClase.Models;

namespace WikiClase.Pages.Posts
{
    public class CreateModel : PageModel
    {
        private readonly WikiClase.Data.AplicationDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(WikiClase.Data.AplicationDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult OnGet()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "nombreCategoria");
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "subCategoria");
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Posts == null || Post == null)
            {
                return Page();
            }

            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostUploadAsync(IFormFile uploadfiles)
        {
            if (!ModelState.IsValid || _context.Posts == null || Post == null)
            {
                //return Page();
                return BadRequest(ModelState);
            }

            string imgext = Path.GetExtension(uploadfiles.FileName);
            if (imgext == ".jpg" || imgext == ".gif" || imgext == ".png")
            {
                var imgsave = Path.Combine(_webHostEnvironment.WebRootPath, "Images", uploadfiles.FileName);
                var stream = new FileStream(imgsave, FileMode.Create);
                await uploadfiles.CopyToAsync(stream);
                stream.Close();

                Post.nombreImagen = uploadfiles.FileName;
                Post.rutaImagen = imgsave;
                await _context.Posts.AddAsync(Post);
                await _context.SaveChangesAsync();
            }
            //_context.Posts.Add(Post);
            //await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
