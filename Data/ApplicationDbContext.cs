using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TarjetaBiblioteca> TarjetasBiblioteca { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }

    }
}
