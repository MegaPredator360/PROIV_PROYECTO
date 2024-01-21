using PROIV_PROYECTO.ModelsDTO;
using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.Interface
{
    public interface IAuditoriaService
    {

        // Desplegables que se usaran para filtraciones en la auditoria
        Task<AuditoriaDropdownsDTO> AuditoriaDropdownsValues();

        // Obtendremos todas las tareas y proyectos
        Task<IEnumerable<Tarea>> ObtenerProyectosTareasAsync(int _proyectoId, int _tareaId, int _estadoId, string _usuarioId);
    }
}
