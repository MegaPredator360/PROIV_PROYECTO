using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.Interface
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
