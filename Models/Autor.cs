using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage="El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }
        public ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}
