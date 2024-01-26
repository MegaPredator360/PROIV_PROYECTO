using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROIV_PROYECTO.Interface;
using Microsoft.AspNetCore.Authorization;
using PROIV_PROYECTO.ModelsDTO.ProyectoDTO;

namespace PROIV_PROYECTO.Controllers
{
    [Authorize(Roles = "Administrador,Gestor")]
    public class ProyectoController : Controller
	{
        // -------- Constructor
        private readonly IProyectoService proyectoService;

        public ProyectoController(IProyectoService _proyectoService)
        {
            proyectoService = _proyectoService;
        }

        // Carga de Index
        public async Task<IActionResult> Index(string _nombreProyecto, int _estadoId)
        {
            ViewData["buscarProyecto"] = _nombreProyecto;

            // Se utilizara para filtrar los datos
            var data = await proyectoService.ObtenerProyectosAsync(_nombreProyecto, _estadoId);
            return View(data);
        }

        // Vista para Crear Proyectos
        public async Task<IActionResult> Create()
        {
            // Se obtienen la lista de estados
            var proyectoDropdown = await proyectoService.ProyectoDropdownValues();

            // Se muestran los nombres, pero se mandará al HTTP Post el Id del estado
            ViewBag.Estados = new SelectList(proyectoDropdown.Estados, "Id", "NombreEstado");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProyectoFormularioDTO _proyectoDTO)
        {
            if (!ModelState.IsValid)
            {
                var proyectoDropdown = await proyectoService.ProyectoDropdownValues();
                ViewBag.Estados = new SelectList(proyectoDropdown.Estados, "Id", "NombreEstado");

                return View(_proyectoDTO);
            }

            await proyectoService.NuevoProyectoAsync(_proyectoDTO);
            return RedirectToAction(nameof(Index));
        }

        // Vista para Actualizar
        public async Task<IActionResult> Update(int Id)
        {
            var proyectoDetalle = await proyectoService.ObtenerProyectoIdAsync(Id);

            if (proyectoDetalle == null)
            {
                return View("NotFound");
            }

            var proyectoDropdown = await proyectoService.ProyectoDropdownValues();
            ViewBag.Estados = new SelectList(proyectoDropdown.Estados, "Id", "NombreEstado");

            return View(proyectoDetalle);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int _proyectoId, ProyectoFormularioDTO _proyectoDTO)
        {
            if (!ModelState.IsValid)
            {
                var proyectoDropdown = await proyectoService.ProyectoDropdownValues();
                ViewBag.Estados = new SelectList(proyectoDropdown.Estados, "Id", "NombreEstado");

                return View(_proyectoDTO);
            }

            await proyectoService.ActualizarProyectoAsync(_proyectoId, _proyectoDTO);
            return RedirectToAction(nameof(Index));
        }

        // Vista con Detalles
        public async Task<IActionResult> Details(int Id)
        {
            var proyectoDetalle = await proyectoService.ObtenerProyectoDetalleAsync(Id);

            if (proyectoDetalle == null)
            {
                return View("Error");
            }

            return View(proyectoDetalle);
        }

        // Vista para Eliminar
        public async Task<IActionResult> Delete(int Id)
        {
            var proyectoDetalle = await proyectoService.ObtenerProyectoDetalleAsync(Id);

            if (proyectoDetalle == null)
            {
                return View("Error");
            }
            return View(proyectoDetalle);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var proyectoDetalle = await proyectoService.ObtenerProyectoIdAsync(Id);

            if (proyectoDetalle == null)
            {
                return View("Error");
            }

            await proyectoService.BorrarProyectoAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
