using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WikiClase.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre de la Categoria")]
        public string nombreCategoria { get; set; }
        [AllowNull]
        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }
    }
}
