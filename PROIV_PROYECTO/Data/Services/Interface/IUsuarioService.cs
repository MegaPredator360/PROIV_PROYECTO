using ProyectoProgra4v2.Models;
using System.Threading;

namespace ProyectoProgra4v2.Data.Services.Interface
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
