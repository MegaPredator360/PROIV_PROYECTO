using Microsoft.EntityFrameworkCore;

namespace PROIV_PROYECTO.ModelsDTO.ProyectoDTO
{
    [Keyless]
    public class ProyectoListaDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? FechaInicio { get; set; }
        public int EstadoId { get; set; }
        public int TareasAsignadas { get; set; }
        public EstadoDTO? EstadoDTOs { get; set; }
    }
}