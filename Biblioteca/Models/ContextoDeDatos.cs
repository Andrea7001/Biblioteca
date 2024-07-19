using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    public class ContextoDeDatos : DbContext
    {
        public ContextoDeDatos(DbContextOptions opciones) : base(opciones)
        {
        }
        public DbSet<Libro> Libros { get; set; }

        public DbSet<Lector> Lectores { get; set; }

        public DbSet<Prestamo> Prestamos { get; set; }
    }
}
