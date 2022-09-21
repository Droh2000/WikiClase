using Microsoft.EntityFrameworkCore;
using WikiClase.Models;

namespace WikiClase.Data
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions options) : base(options) // Aqui sobre escribimos el contructor
        {

        }

        // sobrescribir un metodos (Con la sobreescritura de estos dos podemos usar el DbContext)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Aqui establecemos las relaciones de las llaves foranias

        }

        // Aqui le decimos al entityFramework core las tablas que va a crear o propiedades
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
