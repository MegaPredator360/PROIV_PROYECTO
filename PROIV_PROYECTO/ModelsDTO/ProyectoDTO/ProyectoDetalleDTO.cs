using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.ModelsDTO.ProyectoDTO
{
    [Keyless]
    public class ProyectoDetalleDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? FechaInicio { get; set; }
        public int EstadoId { get; set; }
        public EstadoDTO? EstadoDTOs { get; set; }
        public IEnumerable<TareaListaDTO>? Tareas { get; set; }
    }
}