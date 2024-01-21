using PROIV_PROYECTO.ModelsDTO;

namespace PROIV_PROYECTO.Interface
{
    public interface ITareaService
    {
        Task<IEnumerable<TareaListaDTO>> ObtenerTareaAsync(string _filtrar, string _textoBusqueda);
        Task<TareaDTO> ObtenerTareaIdAsync(int _tareaId);
        Task NuevaTareaAsync(TareaDTO _tareaDTO);
        Task ActualizarTareaAsync(int _tareaId, TareaDTO _tareaDTO);
        Task BorrarTareaAsync(int _tareaId);
        Task<TareaDropdownDTO> TareaDropdownValues();
    }
}
