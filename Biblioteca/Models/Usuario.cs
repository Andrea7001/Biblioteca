using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "La clave debe estar entre 6 y 20 caracteres", MinimumLength = 6)]
        public string? Password { get; set; }

        [NotMapped]
        public bool KeepLoggedIn { get; set; }
    }
}
