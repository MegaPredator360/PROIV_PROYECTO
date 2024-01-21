using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROIV_PROYECTO.ModelsDTO
{
    public class ProyectoDTO
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
}