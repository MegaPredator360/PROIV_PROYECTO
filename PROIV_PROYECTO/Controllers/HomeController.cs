using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROIV_PROYECTO.ModelsDTO;
using PROIV_PROYECTO.Interface;
using System.Diagnostics;

namespace PROIV_PROYECTO.Controllers
{
    public class HomeController : Controller
    {
        // ----------- Constructor
        private readonly ILogger<HomeController> logger;
        private readonly IAuditoriaService auditoriaService;

        public HomeController(ILogger<HomeController> _logger, IAuditoriaService _auditoriaService)
        {
            logger = _logger;
            auditoriaService = _auditoriaService;
        }

        // --------- Carga de Paginas
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Auditoria(int _proyectoId, int _tareaId, int _estadoId, string _usuarioId)
        {
            var data = await auditoriaService.ObtenerAuditoriaAsync(_proyectoId, _tareaId, _estadoId, _usuarioId);

            var auditoriaDropdown = await auditoriaService.AuditoriaDropdownValues();
            ViewBag.Proyectos = new SelectList(auditoriaDropdown.Proyectos, "Id", "Nombre");
            ViewBag.Tareas = new SelectList(auditoriaDropdown.Tareas, "Id", "Nombre");
            ViewBag.Estados = new SelectList(auditoriaDropdown.Estados, "Id", "NombreEstado");
            ViewBag.Usuarios = new SelectList(auditoriaDropdown.Usuarios, "Id", "FullName");

            return View(data);
        }
    }
}