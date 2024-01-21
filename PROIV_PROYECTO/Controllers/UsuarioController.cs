using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROIV_PROYECTO.Interface;
using PROIV_PROYECTO.ModelsDTO;

namespace PROIV_PROYECTO.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : Controller
    {
        public readonly IUsuarioService usuarioService;
        public UsuarioController(IUsuarioService _usuarioService)
        {
            usuarioService = _usuarioService;
        }

        // Vista de Index para Usuarios
        public IActionResult Index(string _filtrar, string _textoBusqueda)
        {
            ViewData["CurrentSearch"] = _textoBusqueda;

            var data = usuarioService.ObtenerUsuario(_filtrar, _textoBusqueda);
            return View(data);
        }

        // Vista para Crear Usuario
        public async Task<IActionResult> Registrar()
        {
            var usuarioDropdown = usuarioService.UsuarioDropdownValues();
            ViewBag.Permisos = new SelectList(usuarioDropdown.Permisos, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioDTO _usuarioDTO)
        {
            if (!ModelState.IsValid) 
            {
                var usuarioDropdown = usuarioService.UsuarioDropdownValues();

                ViewBag.Permisos = new SelectList(usuarioDropdown.Permisos, "Id", "Name");

                return View(_usuarioDTO);
            }

            var result = await usuarioService.NuevoUsuarioAsync(_usuarioDTO);
            TempData["msg"] = result.Message;
            return RedirectToAction("Index");
        }

        // Vista para actualizar usuario
        public IActionResult Actualizar(string _usuarioId)
        {

            var usuarioDetalle = usuarioService.ObtenerUsuarioId(_usuarioId);

            if (usuarioDetalle == null)
            {
                return View("Error");
            }

            var usuarioDropdown = usuarioService.UsuarioDropdownValues();
            ViewBag.Permisos = new SelectList(usuarioDropdown.Permisos, "Id", "Name");

            return View(usuarioDetalle);
        }
        
        [HttpPost]
        public async Task<IActionResult> Actualizar(UsuarioDTO _usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                var usuarioDropdown = usuarioService.UsuarioDropdownValues();
                ViewBag.Permisos = new SelectList(usuarioDropdown.Permisos, "Id", "Name");

                return View(_usuarioDTO);
            }

            await usuarioService.ActualizarUsuarioAsync(_usuarioDTO);
            return RedirectToAction("Index");
        }

        // Vista para borrar usuarios
        public async Task<IActionResult> Borrar(string _usuarioId)
        {
            var usuarioDetalle = usuarioService.ObtenerUsuarioBorrarId(_usuarioId);
            if (usuarioDetalle == null)
            {
                return View("Error");
            }

            return View(usuarioDetalle);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarUsuario(UsuarioDTO _usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                var usuarioDetalle = usuarioService.ObtenerUsuarioId(_usuarioDTO!.Id!);
                return View(usuarioDetalle);
            }

            await usuarioService.BorrarUsuario(_usuarioDTO);
            return RedirectToAction("Index");
        }
    }
}
