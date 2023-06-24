using ProyectoProgra4v2.Models;

namespace ProyectoProgra4v2.Data.Services.Interface
{
    public interface IAuditoriaService
    {
        Task<NewAuditoriaDropdowns> GetNewAuditoriaDropdownsValues();
        Task<IEnumerable<Tarea>> GetAllAsync(int ProyectoId, int TareaId, int EstadoId, string UsuarioId);
    }
}
