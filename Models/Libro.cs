using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Libro
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }

        [Required]
        public int AutorId { get; set; }

        public Autor? Autor { get; set; }

        public List<Categoria> Categoria { get; set; } = new();
        public List<Prestamo>? Prestamos { get; set; } = new(); // Lista de prestamos>
    }
}