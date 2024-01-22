using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.ModelsDTO
{
    [Keyless]
    public class TareaUsuarioListaDTO
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? NombreEstado { get; set; }

        public string? NombreProyecto { get; set; }

        public static explicit operator TareaUsuarioListaDTO(TareaUsuario v)
        {
            throw new NotImplementedException();
        }
    }
}