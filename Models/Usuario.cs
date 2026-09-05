using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage="El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "Debe ingresar el código de la tarjeta")]
        public TarjetaBiblioteca TarjetaBiblioteca  { get; set; }
        public List<Prestamo> Prestamos { get; set; } = new();
    }
}
