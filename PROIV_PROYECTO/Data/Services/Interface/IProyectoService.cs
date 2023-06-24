using ProyectoProgra4v2.Models;

namespace ProyectoProgra4v2.Data.Services.Interface
{
    public interface IProyectoService
    {
        Task<IEnumerable<ProyectoLista>> GetAllAsync(string SearchBy, string SearchString);
        Task<Proyecto> GetByIdAsync(int Id);
        Task<IList<ProyectoDetalle>> GetByIdDetalleAsync(int Id);
        Task AddAsync(ProyectoNew proyectoNew);
        Task UpdateAsync(int Id, ProyectoNew newProyecto);
        Task DeleteAsync(int Id);
        Task<NewProyectoDropdowns> GetNewProyectoDropdownsValues();

    }
}
