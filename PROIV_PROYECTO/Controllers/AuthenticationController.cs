using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROIV_PROYECTO.Interface;
using PROIV_PROYECTO.ModelsDTO;

namespace PROIV_PROYECTO.Controllers
{
    public class AuthenticationController : Controller
    {
        // ----------- Constructor
        private readonly IAuthenticationService authService;

        public AuthenticationController(IAuthenticationService _authService)
        {
            authService = _authService;
        }

        // ------ Carga de paginas
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        // -------- HTTP Metodos
        [HttpPost]
        public async Task<IActionResult> Login(IniciarSesionDTO _iniciarSesionDTO)
        {
            // Carga la pagina para iniciar sesion
            if (!ModelState.IsValid)
            {
                return View(_iniciarSesionDTO);
            }

            // Se envian los datos para verificar
            var result = await authService.IniciarSesionAsync(_iniciarSesionDTO);

            // Si se inicia sesion
            if (result.StatusCode == 1)
            {
                // Sera redirigido a la pagina inicial
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Mostrara error
                TempData["msg"] = result.Message;

                return RedirectToAction(nameof(Login));
            }
        }

        // Authorize = Para poder ingresar, debes de tener la sesion iniciada
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // Se cierra la sesion
            await this.authService.CerrarSesionAsync();

            // Se redirige a la pagina inicial
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(CambiarContrasenaDTO _cambiarContrasenaDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(_cambiarContrasenaDTO);
            }
            
            // Se envian los datos de la contraseña
            var result = await authService.CambiarContrasenaAsync(_cambiarContrasenaDTO, User.Identity!.Name!);
            TempData["msg"] = result.Message;

            return RedirectToAction("Index", "Home");
        }
    }
}
