using ProyectoProgra4v2.Models;

namespace ProyectoProgra4v2.Data.Services.Interface
{
    public interface ITareaService
    {
        Task<IEnumerable<TareaLista>> GetAllAsync(string SearchBy, string SearchString);

        Task<IEnumerable<TareaUsuarioLista>> GetAllUserAsync(string SearchBy, string SearchString, string username);
        Task<Tarea> GetByIdAsync(int Id);
        Task AddAsync(TareaNew tareaNew);
        Task UpdateAsync(int Id, TareaNew newTarea);
        Task UpdateUsuarioAsync(int Id, Tarea tarea);
        Task DeleteAsync(int Id);
        Task<NewTareaDropdowns> GetNewTareaDropdownsValues();
    }
}
