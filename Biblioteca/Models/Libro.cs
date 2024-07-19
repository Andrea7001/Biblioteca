using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Libro
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [Display(Name = "Nombre del Libro")]
        public string? Nombre { get; set; }

        
    }
}
