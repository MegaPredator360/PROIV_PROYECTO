using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoProgra4v2.Data;
using ProyectoProgra4v2.Data.Services.Interface;
using ProyectoProgra4v2.Models;

namespace ProyectoProgra4v2.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : Controller
    {
        public readonly IUsuarioService _service;
        public readonly ApplicationDbContext _context;
        public UsuarioController(IUsuarioService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        // Vista de Index para Usuarios
        public IActionResult Index(string SearchBy, string SearchString)
        {
            ViewData["CurrentSearch"] = SearchString;
            var data = _service.GetUsuarios(SearchBy, SearchString);
            return View(data);
        }

        // Vista para Crear Usuario
        public async Task<IActionResult> Registrar()
        {
            var usuarioDropdownsData = _service.GetNewUsuarioDropdownsValues();
            ViewBag.Roles = new SelectList(usuarioDropdownsData.appRoles, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioDatos usuarioDatos)
        {
            if (!ModelState.IsValid) 
            {
                var usuarioDropdownsData = _service.GetNewUsuarioDropdownsValues();
                ViewBag.Roles = new SelectList(usuarioDropdownsData.appRoles, "Id", "Name");

                return View(usuarioDatos);
            }

            var result = await _service.AddUsuarioAsync(usuarioDatos);
            TempData["msg"] = result.Message;
            return RedirectToAction("Index");
        }

        // Vista para actualizar usuario
        public IActionResult Actualizar(string Id)
        {

            var detalleUsuario = _service.GetUsuarioId(Id);
            if (detalleUsuario == null)
            {
                return View("Error");
            }

            var usuarioDropdownsData = _service.GetNewUsuarioDropdownsValues();
            ViewBag.Roles = new SelectList(usuarioDropdownsData.appRoles, "Id", "Name");

            return View(detalleUsuario);
        }

        
        [HttpPost]
        public async Task<IActionResult> Actualizar(UsuarioActualizar usuarioActualizar)
        {
            if (!ModelState.IsValid)
            {
                var usuarioDropdownsData = _service.GetNewUsuarioDropdownsValues();
                ViewBag.Roles = new SelectList(usuarioDropdownsData.appRoles, "Id", "Name");

                return View(usuarioActualizar);
            }

            var result = await _service.UpdateUsuario(usuarioActualizar);
            return RedirectToAction("Index");
        }

        // Vista para borrar usuarios
        public async Task<IActionResult> Borrar(string Id)
        {
            var detalleUsuario = _service.GetUsuaDeleId(Id);
            if (detalleUsuario == null)
            {
                return View("Error");
            }

            return View(detalleUsuario);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarUsuario(UsuarioActualizar usuarioActualizar)
        {
            if (!ModelState.IsValid)
            {
                var detalleUsuario = _service.GetUsuarioId(usuarioActualizar.Id);
                return View(detalleUsuario);
            }

            var result = await _service.DeleteUsuario(usuarioActualizar);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<IActionResult> RegisterAdmin()
        {
            UsuarioDatos usuarioDatos = new UsuarioDatos
            {
                IdNumber = "NumeroCedula",
                UserName = "NombreUsuario",
                Email = "Correo@correo.com",
                FullName = "NombreCompleto",
                Password = "Contraseña"
            };
            usuarioDatos.Role = "IdRole";
            var result = await this._service.AddUsuarioAsync(usuarioDatos);
            return Ok(result);
        }
    }
}
