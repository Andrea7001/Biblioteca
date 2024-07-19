using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Lector
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(8, ErrorMessage = "Maximo 8 caracteres")]
        [Display(Name = "Nombre del Lector")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [Display(Name = "Apellido del Lector")]

        public string? Apellido { get; set; }

        [Required(ErrorMessage = "El Email es requerido")]
        [MaxLength(100, ErrorMessage = "Maximo 100 caracteres")]
        [Display(Name = "Email del lector")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El Telefono es requerido")]
        [Display(Name = "Telefono del lector")]

        public string? Telefono { get; set; }


        public List<Prestamo>? Prestamos { get; set; }
    }
}
