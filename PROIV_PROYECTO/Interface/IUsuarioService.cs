using PROIV_PROYECTO.Models;
using System.Threading;

namespace PROIV_PROYECTO.Interface
{
    public interface IUsuarioService
    {
        IEnumerable<UsuarioLista> GetUsuarios(string SearchBy, string SearchString);
        UsuarioActualizar GetUsuarioId(string Id);

        UsuarioActualizar GetUsuaDeleId(string Id);
        Task<Status> AddUsuarioAsync(UsuarioDatos usuarioDatos);
        NewUsuarioDropdowns GetNewUsuarioDropdownsValues();
        Task<ApplicationUser> UpdateUsuario(UsuarioActualizar usuarioActualizar);
        Task<ApplicationUser> DeleteUsuario(UsuarioActualizar usuarioActualizar);
    }
}
