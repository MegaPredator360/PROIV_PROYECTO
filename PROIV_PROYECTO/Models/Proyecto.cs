using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProyectoProgra4v2.Models
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


    public class ProyectoNew
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del proyecto es requerido")]
        [Display(Name = "Nombre")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La descripción del proyecto es requerida")]
        [Display(Name = "Descrpción")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Debe de colocar una fecha de inicio")]
        [Display(Name = "Fecha de Inicio")]
        public string? FechaInicio { get; set; }

        [Required]
        [Display(Name = "Estado")]
        [ForeignKey("EstadoId")]
        public int EstadoId { get; set; }
    }

    public class NewProyectoDropdowns
    {
        public NewProyectoDropdowns()
        {
            Estados = new List<Estado>();
        }
        public List<Estado> Estados { get; set; }
    }

    [Keyless]
    public class ProyectoLista
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? FechaInicio { get; set; }
        public string? NombreEstado { get; set; }
        public Int32 TareasAsignadas { get; set; }
    }

    [Keyless]
    public class ProyectoDetalle
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? FechaInicio { get; set; }
        public string? ProyectoEstado { get; set; }
        public int IdTarea { get; set; }
        public string? TareaNombre { get; set; }
        public string? TareaEstado { get; set; }
        public Int32 PersonasAsignadas { get; set; }
    }
}
