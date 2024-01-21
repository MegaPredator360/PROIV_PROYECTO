using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROIV_PROYECTO.ModelsDTO;
using PROIV_PROYECTO.Interface;
using Microsoft.AspNetCore.Authorization;

namespace PROIV_PROYECTO.Controllers
{
    public class TareaController : Controller
    {
        // ---------- Constructor
        private readonly ITareaService tareaService;

        public TareaController(ITareaService _tareaService)
        {
            tareaService = _tareaService;
        }

        [Authorize(Roles = "Administrador,Gestor")]
        public async Task<IActionResult> Index(string _filtrar, string _textoBusqueda)
        {
            ViewData["CurrentSearch"] = _textoBusqueda;

            var data = await tareaService.ObtenerTareaAsync(_filtrar, _textoBusqueda);
            return View(data);
        }


        // Vista para crear Tarea
        [Authorize(Roles = "Administrador,Gestor")]
        public async Task<IActionResult> Create()
        {
            var tareaDropdown = await tareaService.TareaDropdownValues();

            ViewBag.Proyectos = new SelectList(tareaDropdown.Proyectos, "Id", "Nombre");
            ViewBag.Usuarios = new SelectList(tareaDropdown.Usuarios, "Id", "FullName");
            ViewBag.Estados = new SelectList(tareaDropdown.Estados, "Id", "NombreEstado");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TareaDTO _tareaDTO)
        {
            if (!ModelState.IsValid)
            {
                var tareaDropdown = await tareaService.TareaDropdownValues();

                ViewBag.Proyectos = new SelectList(tareaDropdown.Proyectos, "Id", "Nombre");
                ViewBag.Usuarios = new SelectList(tareaDropdown.Usuarios, "Id", "FullName");
                ViewBag.Estados = new SelectList(tareaDropdown.Estados, "Id", "NombreEstado");

                return View(_tareaDTO);
            }

            await tareaService.NuevaTareaAsync(_tareaDTO);
            return RedirectToAction(nameof(Index));
        }

        // Vista para Actualizar
        [Authorize(Roles = "Administrador,Gestor")]
        public async Task<IActionResult> Update(int _tareaId)
        {
            var tareaDetalle = await tareaService.ObtenerTareaIdAsync(_tareaId);

            if (tareaDetalle == null)
            {
                return View("Error");
            }

            var response = new TareaDTO()
            {
                Id = tareaDetalle.Id,
                Nombre = tareaDetalle.Nombre,
                Descripcion = tareaDetalle.Descripcion,
                ProyectoId = tareaDetalle.ProyectoId,
                EstadoId = tareaDetalle.EstadoId,
                AssignedUsersId = tareaDetalle.AssignedUsersId
            };

            var tareaDropdown = await tareaService.TareaDropdownValues();
            ViewBag.Proyectos = new SelectList(tareaDropdown.Proyectos, "Id", "Nombre");
            ViewBag.Usuarios = new SelectList(tareaDropdown.Usuarios, "Id", "FullName");
            ViewBag.Estados = new SelectList(tareaDropdown.Estados, "Id", "NombreEstado");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int _tareaId, TareaDTO _tareaDTO)
        {
            if (!ModelState.IsValid)
            {
                var tareaDropdown = await tareaService.TareaDropdownValues();

                ViewBag.Proyectos = new SelectList(tareaDropdown.Proyectos, "Id", "Nombre");
                ViewBag.Usuarios = new SelectList(tareaDropdown.Usuarios, "Id", "FullName");
                ViewBag.Estados = new SelectList(tareaDropdown.Estados, "Id", "NombreEstado");

                return View(_tareaDTO);
            }
            await tareaService.ActualizarTareaAsync(_tareaId, _tareaDTO);
            return RedirectToAction(nameof(Index));
        }

        // Vista para Detalles
        [Authorize(Roles = "Administrador,Gestor")]
        public async Task<IActionResult> Details(int _tareaId)
        {
            var tareaDetalle = await tareaService.ObtenerTareaIdAsync(_tareaId);

            if (tareaDetalle == null)
            {
                return View("Error");
            }

            return View(tareaDetalle);
        }

        // Vista para Eliminar
        [Authorize(Roles = "Administrador,Gestor")]
        public async Task<IActionResult> Delete(int _tareaId)
        {
            var tareaDetalle = await tareaService.ObtenerTareaIdAsync(_tareaId);

            if (tareaDetalle == null)
            {
                return View("Error");
            }
            return View(tareaDetalle);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int _tareaId)
        {
            var tareaDetalle = await tareaService.ObtenerTareaIdAsync(_tareaId);

            if (tareaDetalle == null)
            {
                return View("Error");
            }

            await tareaService.BorrarTareaAsync(_tareaId);
            return RedirectToAction(nameof(Index));
        }
    }
}
