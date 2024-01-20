using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.Interface
{
    public interface IAuthenticationService
    {
        Task<Status> LoginAsync(Login login);
        Task LogoutAsync();
        Task<Status> ChangePasswordAsync(ChangePassword changePassword, string username);
    }
}
