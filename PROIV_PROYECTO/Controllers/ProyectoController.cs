using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROIV_PROYECTO.ModelsDTO;
using PROIV_PROYECTO.Interface;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Index(string _filtrar, string _textoBusqueda)
        {
            ViewData["CurrentSearch"] = _filtrar;

            // Se utilizara para filtrar los datos
            var data = await proyectoService.ObtenerProyectosAsync(_filtrar, _textoBusqueda);
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
        public async Task<IActionResult> Create(ProyectoDTO _proyectoDTO)
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
        public async Task<IActionResult> Update(int _proyectoId)
        {
            var proyectoDetalle = await proyectoService.ObtenerProyectoIdAsync(_proyectoId);

            if (proyectoDetalle == null)
            {
                return View("NotFound");
            }

            var response = new ProyectoDTO()
            {
                Id = proyectoDetalle.Id,
                Nombre = proyectoDetalle.Nombre,
                Descripcion = proyectoDetalle.Descripcion,
                FechaInicio = proyectoDetalle.FechaInicio,
                EstadoId = proyectoDetalle.EstadoId
            };

            var proyectoDropdown = await proyectoService.ProyectoDropdownValues();
            ViewBag.Estados = new SelectList(proyectoDropdown.Estados, "Id", "NombreEstado");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int _proyectoId, ProyectoDTO _proyectoDTO)
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

        // Vista para Detalles con Tareas
        public async Task<IActionResult> DetailsTarea(int _proyectoId)
        {
            var proyectoDetalle = await proyectoService.ObtenerProyectoDetalleAsync(_proyectoId);

            if (proyectoDetalle == null)
            {
                return View("Error");
            }

            return View(proyectoDetalle);
        }

        public async Task<IActionResult> Details(int _proyectoId)
        {
            var proyectoDetalle = await proyectoService.ObtenerProyectoIdAsync(_proyectoId);

            if (proyectoDetalle == null)
            {
                return View("Error");
            }
            return View(proyectoDetalle);
        }

        // Vista para Eliminar
        public async Task<IActionResult> Delete(int _proyectoId)
        {
            var proyectoDetalle = await proyectoService.ObtenerProyectoIdAsync(_proyectoId);

            if (proyectoDetalle == null)
            {
                return View("Error");
            }
            return View(proyectoDetalle);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int _proyectoId)
        {
            var proyectoDetalle = await proyectoService.ObtenerProyectoIdAsync(_proyectoId);

            if (proyectoDetalle == null)
            {
                return View("Error");
            }

            await proyectoService.BorrarProyectoAsync(_proyectoId);
            return RedirectToAction(nameof(Index));
        }
    }
}
