using Microsoft.AspNetCore.Identity;
using ProyectoProgra4v2.Data.Services.Interface;
using ProyectoProgra4v2.Models;
using System.Security.Claims;

namespace ProyectoProgra4v2.Data.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        // Metodo para Inicio de Sesion
        public async Task<Status> LoginAsync(Login login)
        {
            var status = new Status();
            var user = await _userManager.FindByNameAsync(login.UserName!);

            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Usuario Invalido";
                return status;
            }

            if (!await _userManager.CheckPasswordAsync(user, login.Password!))
            {
                status.StatusCode = 0;
                status.Message = "Contraseña Invalida";
                return status;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, login.Password!, false, true);

            if (signInResult.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                status.StatusCode = 1;
                status.Message = null;
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "El usuario ha sido bloqueado";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error al iniciar sesion";
            }

            return status;
        }

        // Metodo para Cerrar Sesion
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        // Metodo para Cambiar Contraseña desde Usuario
        public async Task<Status> ChangePasswordAsync(ChangePassword changePassword, string username)
        {
            var status = new Status();
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                status.Message = "El usuario no existe";
                status.StatusCode = 0;
                return status;
            }

            var result = await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword!, changePassword.NewPassword!);

            if (result.Succeeded)
            {
                status.Message = null;
                status.StatusCode = 1;
            }
            else
            {
                status.Message = "Un error ha ocurrido";
                status.StatusCode = 0;
            }

            return status;
        }
    }
}
