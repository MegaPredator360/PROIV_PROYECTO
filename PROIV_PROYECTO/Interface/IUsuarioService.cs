using PROIV_PROYECTO.ModelsDTO;
using System.Threading;

namespace PROIV_PROYECTO.Interface
{
    public interface IUsuarioService
    {
        IEnumerable<UsuarioListaDTO> ObtenerUsuario(string _filtrar, string _textoBusqueda);
        UsuarioDTO ObtenerUsuarioId(string _usuarioId);

        UsuarioDTO ObtenerUsuarioBorrarId(string _usuarioId);
        Task<StatusDTO> NuevoUsuarioAsync(UsuarioDTO _usuarioDTO);
        UsuarioDropdownDTO UsuarioDropdownValues();
        Task ActualizarUsuarioAsync(UsuarioDTO _usuarioDTO);
        Task BorrarUsuario(UsuarioDTO _usuarioDTO);
    }
}
