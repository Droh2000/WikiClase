using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WikiClase.Data;
using WikiClase.Models;

namespace WikiClase.Pages.Tags
{
    public class IndexModel : PageModel
    {
        private readonly WikiClase.Data.AplicationDBContext _context;

        public IndexModel(WikiClase.Data.AplicationDBContext context)
        {
            _context = context;
        }

        public IList<Tag> Tag { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Tags != null)
            {
                Tag = await _context.Tags
                .Include(t => t.Categoria).ToListAsync();
            }
        }
    }
}
