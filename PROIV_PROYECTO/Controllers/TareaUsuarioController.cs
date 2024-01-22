using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROIV_PROYECTO.Interface;
using PROIV_PROYECTO.Models;
using PROIV_PROYECTO.ModelsDTO;
using PROIV_PROYECTO.Services;

namespace PROIV_PROYECTO.Controllers
{
    public class TareaUsuarioController : Controller
    {

        // ---------- Constructor
        private readonly ITareaUsuarioService tuService;
        private readonly ITareaService tareaService;
        private readonly UserManager<Usuario> userManager;

        public TareaUsuarioController(ITareaUsuarioService _tuService, TareaService _tareaService, UserManager<Usuario> _userManager)
        {
            tuService = _tuService;
            tareaService = _tareaService;
            userManager = _userManager;
        }

        [Authorize(Roles = "Usuario")]
        public async Task<IActionResult> UsuarioIndex(string _filtrar, string _textoBusqueda)
        {
            var userName = userManager.GetUserId(HttpContext.User);
            ViewData["CurrentSearch"] = _textoBusqueda;
            var data = await tuService.ObtenerTareaUsuarioAsync(_filtrar, _textoBusqueda, userName!);
            return View(data);
        }

        [Authorize(Roles = "Usuario")]
        public async Task<IActionResult> UsuarioDetails(int _tareaId)
        {
            var tareaDetalle = await tareaService.ObtenerTareaIdAsync(_tareaId);

            if (tareaDetalle == null)
            {
                return View("Error");
            }

            var tareaDropdown = await tareaService.TareaDropdownValues();

            ViewBag.Proyectos = new SelectList(tareaDropdown.Proyectos, "Id", "Nombre");
            ViewBag.Usuarios = new SelectList(tareaDropdown.Usuarios, "Id", "FullName");
            ViewBag.Estados = new SelectList(tareaDropdown.Estados, "Id", "NombreEstado");

            return View(tareaDetalle);
        }

        [HttpPost]
        public async Task<IActionResult> UsuarioDetails(int _tareaId, TareaDTO _tareaDTO)
        {
            await tuService.ActualizarTareaAsync(_tareaId, _tareaDTO);
            return RedirectToAction(nameof(UsuarioIndex));
        }
    }
}