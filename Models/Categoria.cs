using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage="El nombre es obligatorio")]
        [StringLength(30,ErrorMessage="El nombre debe tener menos de 30 caracteres")]
        public string Nombre { get; set; }
        public ICollection<Libro> Libros { get; set; } = new List<Libro>(); //relacion uno a muchos>
    }
}
