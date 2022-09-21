using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WikiClase.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string Autor { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        // Imagen
        [Display(Name = "Nombre del Archivo")]
        public string? nombreImagen { get; set; }
        public string? rutaImagen { get; set; }

        // Relacion
        [Required]
        [ForeignKey("Categoria")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }

        [Required]
        [ForeignKey("Tag")]
        [Display(Name = "Tag")]
        public int TagId { get; set; }
        public virtual Tag? Tag { get; set; }
    }

}
