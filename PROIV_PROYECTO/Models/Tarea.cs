using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PROIV_PROYECTO.Models
{
    public class Tarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        [ForeignKey("ProyectoId")]
        public int ProyectoId { get; set; }

        [ForeignKey("EstadoId")]
        public int EstadoId { get; set; }

        public List<TareaUsuario> TareasUsuarios { get; set; } = null!;

        public Proyecto Proyecto { get; set; } = null!;
        public Estado Estado { get; set; } = null!;
    }
}