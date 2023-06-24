using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoProgra4v2.Models;
using ProyectoProgra4v2.Data.Services.Interface;
using System.Diagnostics;

namespace ProyectoProgra4v2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuditoriaService _service;

        public HomeController(ILogger<HomeController> logger, IAuditoriaService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Auditoria(int ProyectoId, int TareaId, int EstadoId, string UsuarioId)
        {
            var data = await _service.GetAllAsync(ProyectoId, TareaId, EstadoId, UsuarioId);

            var auditoriaDropdownsData = await _service.GetNewAuditoriaDropdownsValues();
            ViewBag.Proyectos = new SelectList(auditoriaDropdownsData.Proyectos, "Id", "Nombre");
            ViewBag.Tareas = new SelectList(auditoriaDropdownsData.Tareas, "Id", "Nombre");
            ViewBag.Estados = new SelectList(auditoriaDropdownsData.Estados, "Id", "NombreEstado");
            ViewBag.Usuarios = new SelectList(auditoriaDropdownsData.Usuarios, "Id", "FullName");

            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}