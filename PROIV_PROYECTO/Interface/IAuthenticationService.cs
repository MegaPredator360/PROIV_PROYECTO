using PROIV_PROYECTO.Models;
using PROIV_PROYECTO.ModelsDTO;

namespace PROIV_PROYECTO.Interface
{
    public interface IAuthenticationService
    {
        Task<StatusDTO> IniciarSesionAsync(IniciarSesionDTO _iniciarSesionDTO);
        Task CerrarSesionAsync();
        Task<StatusDTO> CambiarContrasenaAsync(CambiarContrasenaDTO _cambiarContrasenaDTO, string _userName);
    }
}
