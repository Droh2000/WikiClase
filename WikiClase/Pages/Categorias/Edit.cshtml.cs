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

namespace WikiClase.Pages.Categorias
{
    public class EditModel : PageModel
    {
        private readonly WikiClase.Data.AplicationDBContext _context;

        public EditModel(WikiClase.Data.AplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Categoria Categoria { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoria =  await _context.Categorias.FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }
            Categoria = categoria;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(Categoria.Id))
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

        private bool CategoriaExists(int id)
        {
          return (_context.Categorias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
