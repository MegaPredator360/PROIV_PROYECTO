using Microsoft.EntityFrameworkCore;

namespace PROIV_PROYECTO.ModelsDTO
{
    [Keyless]
    public class TareaUsuarioListaDTO
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? NombreEstado { get; set; }

        public string? NombreProyecto { get; set; }
    }
}