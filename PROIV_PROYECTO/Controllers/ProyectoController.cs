using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoProgra4v2.Models;
using ProyectoProgra4v2.Data.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoProgra4v2.Controllers
{
    [Authorize(Roles = "Administrador,Gestor")]
    public class ProyectoController : Controller
	{
        private readonly IProyectoService _service;

        public ProyectoController(IProyectoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string SearchBy, string SearchString)
        {
            ViewData["CurrentSearch"] = SearchString;
            var data = await _service.GetAllAsync(SearchBy, SearchString);
            return View(data);
        }

        // Vista para Crear Proyectos
        public async Task<IActionResult> Create()
        {
            var proyectoDropdownsData = await _service.GetNewProyectoDropdownsValues();
            ViewBag.Estados = new SelectList(proyectoDropdownsData.Estados, "Id", "NombreEstado");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProyectoNew proyectoNew)
        {
            if (!ModelState.IsValid)
            {
                var proyectoDropdownsData = await _service.GetNewProyectoDropdownsValues();
                ViewBag.Estados = new SelectList(proyectoDropdownsData.Estados, "Id", "NombreEstado");

                return View(proyectoNew);
            }
            await _service.AddAsync(proyectoNew);
            return RedirectToAction(nameof(Index));
        }

        // Vista para Actualizar
        public async Task<IActionResult> Update(int Id)
        {
            var proyectoDetails = await _service.GetByIdAsync(Id);
            if (proyectoDetails == null)
            {
                return View("NotFound");
            }

            var response = new ProyectoNew()
            {
                Id = proyectoDetails.Id,
                Nombre = proyectoDetails.Nombre,
                Descripcion = proyectoDetails.Descripcion,
                FechaInicio = proyectoDetails.FechaInicio,
                EstadoId = proyectoDetails.EstadoId
            };

            var proyectoDropdownsData = await _service.GetNewProyectoDropdownsValues();
            ViewBag.Estados = new SelectList(proyectoDropdownsData.Estados, "Id", "NombreEstado");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int Id, ProyectoNew proyectoNew)
        {
            if (!ModelState.IsValid)
            {
                var proyectoDropdownsData = await _service.GetNewProyectoDropdownsValues();
                ViewBag.Estados = new SelectList(proyectoDropdownsData.Estados, "Id", "NombreEstado");

                return View(proyectoNew);
            }
            await _service.UpdateAsync(Id, proyectoNew);
            return RedirectToAction(nameof(Index));
        }

        // Vista para Detalles con Tareas
        public async Task<IActionResult> DetailsTarea(int Id)
        {
            var proyectoDetails = await _service.GetByIdDetalleAsync(Id);

            if (proyectoDetails == null)
            {
                return View("Error");
            }

            return View(proyectoDetails);
        }

        public async Task<IActionResult> Details(int id)
        {
            var proyectoDetails = await _service.GetByIdAsync(id);

            if (proyectoDetails == null)
            {
                return View("Error");
            }
            return View(proyectoDetails);
        }

        // Vista para Eliminar
        public async Task<IActionResult> Delete(int id)
        {
            var proyectoDetails = await _service.GetByIdAsync(id);

            if (proyectoDetails == null)
            {
                return View("Error");
            }
            return View(proyectoDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null)
            {
                return View("Error");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
