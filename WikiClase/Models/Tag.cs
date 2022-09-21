using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WikiClase.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Subcategoria")]
        public string subCategoria { get; set; }
        //Relacion 
        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }

        //En el video el codigo de Country es String

    }
}
