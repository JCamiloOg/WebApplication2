using WebApplication2.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication2.Data
{
    public class SeedData
    {

        public static void Poblar(ApplicationDbContext context)
        {
            if (context.Autores.Any()) return;

            //Autores
            var autor1 = new Autor { Nombre = "Gabriel Garcia Marquez" };
            var autor2 = new Autor { Nombre = "Julio Verne" };
            //Categorias
            var cat1 = new Categoria { Nombre = "Fantasia" };
            var cat2 = new Categoria { Nombre = "Clasicos" };
            var cat3 = new Categoria { Nombre = "Ciencia Ficción" };
            //Libros
            var libro1 = new Libro { Titulo = "Cien anos de Soledad", Autor = autor1 };
            var libro2 = new Libro { Titulo = "El amor en los tiempos del colera", Autor = autor1 };
            var libro3 = new Libro { Titulo = "Viaje al centro de la tierra", Autor = autor2 };

            libro1.Categoria.Add(cat1);
            libro1.Categoria.Add(cat2);
            libro2.Categoria.Add(cat2);
            libro3.Categoria.Add(cat3);
            //Relacion de usuarios con tarjeta (1:1)
            var usuario1 = new Usuario
            {
                Nombre = "Juan Perez",
                TarjetaBiblioteca = new TarjetaBiblioteca { Codigo = "TARJ-001" }
            };

            var usuario2 = new Usuario
            {
                Nombre = "María López",
                TarjetaBiblioteca = new TarjetaBiblioteca { Codigo = "TARJ-002" }
            };

            var prestamo1 = new Prestamo
            {
                Libro = libro1,
                Usuario = usuario1,
                FechaPrestamo = DateTime.Now.AddDays(-2)
            };
            var prestamo2 = new Prestamo
            {
                Libro = libro1,
                Usuario = usuario1,
                FechaPrestamo = DateTime.Now.AddDays(-1)
            };

            var prestamo3 = new Prestamo
            {
                Libro = libro2,
                Usuario = usuario1,
                FechaPrestamo = DateTime.Now

            };

            context.AddRange(
                autor1, autor2,
                cat1, cat2, cat3,
                libro1, libro2, libro3,
                usuario1, usuario2,
                prestamo1,prestamo2, prestamo3
                );

            context.SaveChanges();
        }
    }
}
