using ProyectoProgra4v2.Models;

namespace ProyectoProgra4v2.Data.Services.Interface
{
    public interface IAuthenticationService
    {
        Task<Status> LoginAsync(Login login);
        Task LogoutAsync();
        Task<Status> ChangePasswordAsync(ChangePassword changePassword, string username);
    }
}
