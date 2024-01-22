using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.ModelsDTO
{
    [Keyless]
    public class TareaListaDTO
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? NombreEstado { get; set; }

        public string? NombreProyecto { get; set; }
        public Int32 PersonasAsignadas { get; set; }
    }
}