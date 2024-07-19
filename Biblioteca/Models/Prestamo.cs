using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    public class Prestamo
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Libro")]
        [Required(ErrorMessage = "El libro es requerido")]
        [Display(Name = "Libro")]
        public int LibroId { get; set; }

        [ForeignKey("Lector")]
        [Required(ErrorMessage = "El lector  es requerido")]
        [Display(Name = "Lector")]
        public int LectorId { get; set; }


        [Required(ErrorMessage = "El dia de prestamo es requerido")]
        [Display(Name = "Dia de prestamo")]
        [DataType(DataType.Date)]
        public DateTime DiaPrestamo { get; set; }

        [Required(ErrorMessage = "El dia de devolucion  es requerido")]
        [Display(Name = "Dia de devolucion")]
        [DataType(DataType.Date)]
        public DateTime DiaDevolucion { get; set; }


        //propiedades de navegacion 

        public Libro? Libro { get; set; }
        public Lector? Lector { get; set; }

    }
}