using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.Interface
{
    public interface IAuditoriaService
    {
        Task<NewAuditoriaDropdowns> GetNewAuditoriaDropdownsValues();
        Task<IEnumerable<Tarea>> GetAllAsync(int ProyectoId, int TareaId, int EstadoId, string UsuarioId);
    }
}
