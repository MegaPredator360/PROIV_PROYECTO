namespace PROIV_PROYECTO.Models
{
    public class TareaUsuario
    {
        public string? UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public int TareaId { get; set; }
        public Tarea Tarea { get; set; } = null!;
    }
}
