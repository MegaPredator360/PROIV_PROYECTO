namespace ProyectoProgra4v2.Models
{
    public class TareaUsuario
    {
        public string? UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public int TareaId { get; set; }
        public Tarea Tarea { get; set; } = null!;
    }
}
