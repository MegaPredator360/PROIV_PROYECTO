using PROIV_PROYECTO.ModelsDTO.ProyectoDTO;
using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.Interface
{
    public interface IProyectoService
    {
        Task<IEnumerable<ProyectoListaDTO>> ObtenerProyectosAsync(string _nombreProyecto, int _estadoId);
        Task<ProyectoFormularioDTO> ObtenerProyectoIdAsync(int _proyectoId);
        Task<ProyectoDetalleDTO> ObtenerProyectoDetalleAsync(int _proyectoId);
        Task NuevoProyectoAsync(ProyectoFormularioDTO _proyectoDTO);
        Task ActualizarProyectoAsync(int _proyectoId, ProyectoFormularioDTO _proyectoDTO);
        Task BorrarProyectoAsync(int _proyectoId);
        Task<ProyectoDropdownDTO> ProyectoDropdownValues();

    }
}
