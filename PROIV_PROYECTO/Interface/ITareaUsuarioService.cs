using PROIV_PROYECTO.ModelsDTO;

namespace PROIV_PROYECTO.Interface
{
    public interface ITareaUsuarioService
    {
        Task<IEnumerable<TareaUsuarioListaDTO>> ObtenerTareaUsuarioAsync(string _filtrar, string _textoBusqueda, string _userName);

        Task ActualizarTareaAsync(int _tareaId, TareaDTO _tareaDTO);
    }
}