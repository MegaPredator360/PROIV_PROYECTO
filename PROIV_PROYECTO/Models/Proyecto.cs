using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PROIV_PROYECTO.Models
{
	public class Proyecto
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? Nombre { get; set; }
		public string? Descripcion { get; set; }
		public string? FechaInicio { get; set; }

        [ForeignKey("EstadoId")]
        public int EstadoId { get; set; }
        public List<Tarea> Tareas { get; set; } = null!;
        public Estado Estado { get; set; } = null!;
    }
}
