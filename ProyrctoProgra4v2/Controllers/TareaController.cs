using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using ProyectoProgra4v2.Models;
using ProyectoProgra4v2.Data.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ProyectoProgra4.Controllers
{
    public class TareaController : Controller
    {
        private readonly ITareaService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public TareaController(ITareaService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [Authorize(Roles = "Usuario")]
        public async Task<IActionResult> UsuarioDetails(int Id)
        {
            var tareaDetails = await _service.GetByIdAsync(Id);
            if (tareaDetails == null)
            {
                return View("Error");
            }

            var tareaDropdownsData = await _service.GetNewTareaDropdownsValues();
            ViewBag.Proyectos = new SelectList(tareaDropdownsData.Proyectos, "Id", "Nombre");
            ViewBag.Usuarios = new SelectList(tareaDropdownsData.Usuarios, "Id", "FullName");
            ViewBag.Estados = new SelectList(tareaDropdownsData.Estados, "Id", "NombreEstado");

            return View(tareaDetails);
        }

        [HttpPost]
        public async Task<IActionResult> UsuarioDetails(int Id, Tarea tarea)
        {
            await _service.UpdateUsuarioAsync(Id, tarea);
            return RedirectToAction(nameof(UsuarioIndex));
        }

        [Authorize(Roles = "Usuario")]
        public async Task<IActionResult> UsuarioIndex(string SearchBy, string SearchString)
        {
            var username = _userManager.GetUserId(HttpContext.User);
            ViewData["CurrentSearch"] = SearchString;
            var data = await _service.GetAllUserAsync(SearchBy, SearchString, username);
            return View(data);
        }

        [Authorize(Roles = "Administrador,Gestor")]
        public async Task<IActionResult> Index(string SearchBy, string SearchString)
        {
            ViewData["CurrentSearch"] = SearchString;
            var data = await _service.GetAllAsync(SearchBy, SearchString);
            return View(data);
        }


        // Vista para crear Tarea
        [Authorize(Roles = "Administrador,Gestor")]
        public async Task<IActionResult> Create()
        {
            var tareaDropdownsData = await _service.GetNewTareaDropdownsValues();
            ViewBag.Proyectos = new SelectList(tareaDropdownsData.Proyectos, "Id", "Nombre");
            ViewBag.Usuarios = new SelectList(tareaDropdownsData.Usuarios, "Id", "FullName");
            ViewBag.Estados = new SelectList(tareaDropdownsData.Estados, "Id", "NombreEstado");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TareaNew tareaNew)
        {
            if (!ModelState.IsValid)
            {
                var tareaDropdownsData = await _service.GetNewTareaDropdownsValues();
                ViewBag.Proyectos = new SelectList(tareaDropdownsData.Proyectos, "Id", "Nombre");
                ViewBag.Usuarios = new SelectList(tareaDropdownsData.Usuarios, "Id", "FullName");
                ViewBag.Estados = new SelectList(tareaDropdownsData.Estados, "Id", "NombreEstado");

                return View(tareaNew);
            }

            await _service.AddAsync(tareaNew);
            return RedirectToAction(nameof(Index));
        }

        // Vista para Actualizar
        [Authorize(Roles = "Administrador,Gestor")]
        public async Task<IActionResult> Update(int Id)
        {
            var tareaDetails = await _service.GetByIdAsync(Id);
            if (tareaDetails == null)
            {
                return View("Error");
            }

            var response = new TareaNew()
            {
                Id = tareaDetails.Id,
                Nombre = tareaDetails.Nombre,
                Descripcion = tareaDetails.Descripcion,
                ProyectoId = tareaDetails.ProyectoId,
                EstadoId = tareaDetails.EstadoId,
                UserIds = tareaDetails.TareasUsuarios.Select(n => n.UsuarioId).ToList()
            };

            var tareaDropdownsData = await _service.GetNewTareaDropdownsValues();
            ViewBag.Proyectos = new SelectList(tareaDropdownsData.Proyectos, "Id", "Nombre");
            ViewBag.Usuarios = new SelectList(tareaDropdownsData.Usuarios, "Id", "FullName");
            ViewBag.Estados = new SelectList(tareaDropdownsData.Estados, "Id", "NombreEstado");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int Id, TareaNew tareaNew)
        {
            if (!ModelState.IsValid)
            {
                var tareaDropdownsData = await _service.GetNewTareaDropdownsValues();
                ViewBag.Proyectos = new SelectList(tareaDropdownsData.Proyectos, "Id", "Nombre");
                ViewBag.Usuarios = new SelectList(tareaDropdownsData.Usuarios, "Id", "FullName");
                ViewBag.Estados = new SelectList(tareaDropdownsData.Estados, "Id", "NombreEstado");

                return View(tareaNew);
            }
            await _service.UpdateAsync(Id, tareaNew);
            return RedirectToAction(nameof(Index));
        }

        // Vista para Detalles
        [Authorize(Roles = "Administrador,Gestor")]
        public async Task<IActionResult> Details(int Id)
        {
            var tareaDetails = await _service.GetByIdAsync(Id);

            if (tareaDetails == null)
            {
                return View("Error");
            }

            return View(tareaDetails);
        }

        // Vista para Eliminar
        [Authorize(Roles = "Administrador,Gestor")]
        public async Task<IActionResult> Delete(int id)
        {
            var tareaDetails = await _service.GetByIdAsync(id);

            if (tareaDetails == null)
            {
                return View("Error");
            }
            return View(tareaDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tareaDetails = await _service.GetByIdAsync(id);
            if (tareaDetails == null)
            {
                return View("Error");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
