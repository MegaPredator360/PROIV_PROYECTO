using PROIV_PROYECTO.Models;
using PROIV_PROYECTO.ModelsDTO;

namespace PROIV_PROYECTO.Interface
{
    public interface IProyectoService
    {
        Task<IEnumerable<ProyectoListaDTO>> ObtenerProyectosAsync(string _filtrar, string _textoBusqueda);
        Task<ProyectoDTO> ObtenerProyectoIdAsync(int _proyectoId);
        Task<IList<ProyectoDetalleDTO>> ObtenerProyectoDetalleAsync(int _proyectoId);
        Task NuevoProyectoAsync(ProyectoDTO _proyectoDTO);
        Task ActualizarProyectoAsync(int _proyectoId, ProyectoDTO _proyectoDTO);
        Task BorrarProyectoAsync(int _proyectoId);
        Task<ProyectoDropdownDTO> ProyectoDropdownValues();

    }
}
