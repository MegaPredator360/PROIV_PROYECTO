using Microsoft.EntityFrameworkCore;

namespace PROIV_PROYECTO.ModelsDTO
{
    [Keyless]
    public class ProyectoDetalleDTO
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