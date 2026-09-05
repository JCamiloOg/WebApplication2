namespace WebApplication2.Models
{
    public class TarjetaBiblioteca
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; } 
    }
}
