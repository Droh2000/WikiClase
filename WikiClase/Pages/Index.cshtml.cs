using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WikiClase.Data;
using WikiClase.Models;

namespace WikiClase.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;
        private readonly AplicationDBContext _cc;

        public IndexModel(AplicationDBContext cc)
        {
            _cc = cc;
        }

        public IEnumerable<Categoria> getCategorias { get; set; }
        public IEnumerable<Post> getPosts { get; set; }
        public IEnumerable<Tag> getTags { get; set; }

        public void OnGet()
        {
            getCategorias = _cc.Categorias.ToList();
            getTags = _cc.Tags.ToList();
            getPosts = _cc.Posts.ToList();
        }
    }
}