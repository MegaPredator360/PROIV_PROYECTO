using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using PROIV_PROYECTO.Interface;
using PROIV_PROYECTO.Models;
using PROIV_PROYECTO.ModelsDTO;

namespace PROIV_PROYECTO.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        // Se crea el constructor
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<Permiso> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AuthenticationService(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager, RoleManager<Permiso> _roleManager)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            signInManager = _signInManager;
        }

        // Metodo para Inicio de Sesion
        public async Task<StatusDTO> IniciarSesionAsync(IniciarSesionDTO _iniciarSesionDTO)
        {
            var status = new StatusDTO();

            // Se busca al usuario
            var user = await userManager.FindByNameAsync(_iniciarSesionDTO.UserName!);

            // Si el usuario regresa nulo
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Usuario Invalido";
                return status;
            }

            // Si la contraseña del usuario es invalida
            if (!await userManager.CheckPasswordAsync(user, _iniciarSesionDTO.Contrasena!))
            {
                status.StatusCode = 0;
                status.Message = "Contraseña Invalida";
                return status;
            }

            // Se inicia sesion
            var signInResult = await signInManager.PasswordSignInAsync(user, _iniciarSesionDTO.Contrasena!, false, true);

            if (signInResult.Succeeded)
            {
                // Se busca los roles asociados al usuario
                var userRoles = await userManager.GetRolesAsync(user);

                // Se crea el Token de Inicio de Sesion
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                };

                // Se identificará el rol del usuario
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
        public async Task CerrarSesionAsync()
        {
            await signInManager.SignOutAsync();
        }

        // Metodo para Cambiar Contraseña con la sesion iniciada
        public async Task<StatusDTO> CambiarContrasenaAsync(CambiarContrasenaDTO _cambiarContrasenaDTO, string _userName)
        {
            var status = new StatusDTO();

            // Se busca al usuario
            var user = await userManager.FindByNameAsync(_userName);

            // Si el usuario no fue encontrado
            if (user == null)
            {
                status.Message = "El usuario no existe";
                status.StatusCode = 0;
                return status;
            }

            // Se procede a cambiar la contraseña
            var result = await userManager.ChangePasswordAsync(user, _cambiarContrasenaDTO.ContrasenaActual!, _cambiarContrasenaDTO.ContrasenaNueva!);

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
