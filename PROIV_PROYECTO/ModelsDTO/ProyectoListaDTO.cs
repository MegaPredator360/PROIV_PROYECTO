using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.ModelsDTO
{
    [Keyless]
    public class ProyectoListaDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? FechaInicio { get; set; }
        public string? NombreEstado { get; set; }
        public Int32 TareasAsignadas { get; set; }

        public static explicit operator ProyectoListaDTO(Proyecto v)
        {
            throw new NotImplementedException();
        }
    }
}