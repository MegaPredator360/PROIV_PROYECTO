using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROIV_PROYECTO.ModelsDTO
{
    public class TareaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la tarea es requerida")]
        [Display(Name = "Nombre")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion de la tarea es requerida")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Display(Name = "Estado")]
        [ForeignKey("EstadoId")]
        public int EstadoId { get; set; }

        [Required(ErrorMessage = "No hay proyectos para asignar la tarea")]
        [ForeignKey("ProyectoId")]
        public int ProyectoId { get; set; }

        [Required(ErrorMessage = "Se necesitan asignar Usuarios a la tarea")]
        public List<string>? AssignedUsersId { get; set; }
    }
}