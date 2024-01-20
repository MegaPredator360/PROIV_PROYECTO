using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Contexts;
using PROIV_PROYECTO.Interface;
using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.Services
{
    public class AuditoriaService : IAuditoriaService
    {

        // Creamos una variable que hará uso de contexto
        private readonly ProjectsContext projectsContext;

        // Constructor
        public AuditoriaService(ProjectsContext _projectsContext)
        {
            projectsContext = _projectsContext;
        }

        public async Task<NewAuditoriaDropdowns> GetNewAuditoriaDropdownsValues()
        {
            var response = new NewAuditoriaDropdowns()
            {
                Proyectos = await _context.Proyectos.OrderBy(n => n.Nombre).ToListAsync(),
                Tareas = await _context.Tareas.OrderBy(n => n.Nombre).ToListAsync(),
                Estados = await _context.Estados.OrderBy(n => n.NombreEstado).ToListAsync(),
                Usuarios = await _context.Usuarios.OrderBy(n => n.FullName).ToListAsync(),
            };

            return response;
        }

        public async Task<IEnumerable<Tarea>> GetAllAsync(int ProyectoId, int TareaId, int EstadoId, string UsuarioId)
        {
            if (ProyectoId == 0 && TareaId == 0 && EstadoId == 0 && UsuarioId == null)
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId != 0 && TareaId == 0 && EstadoId == 0 && UsuarioId == "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(ProyectoId))
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId != 0 && TareaId != 0 && EstadoId == 0 && UsuarioId == "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(ProyectoId))
                    .Where(t => t.Id.Equals(TareaId))
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId != 0 && TareaId == 0 && EstadoId != 0 && UsuarioId == "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(ProyectoId))
                    .Where(e => e.EstadoId.Equals(EstadoId))
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId != 0 && TareaId == 0 && EstadoId == 0 && UsuarioId != "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(ProyectoId))
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(UsuarioId)))
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId != 0 && TareaId != 0 && EstadoId != 0 && UsuarioId == "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(ProyectoId))
                    .Where(t => t.Id.Equals(TareaId))
                    .Where(e => e.EstadoId.Equals(EstadoId))
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId != 0 && TareaId == 0 && EstadoId != 0 && UsuarioId != "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(ProyectoId))
                    .Where(e => e.EstadoId.Equals(EstadoId))
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(UsuarioId)))
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId != 0 && TareaId != 0 && EstadoId != 0 && UsuarioId != "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(ProyectoId))
                    .Where(t => t.Id.Equals(TareaId))
                    .Where(e => e.EstadoId.Equals(EstadoId))
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(UsuarioId)))
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId == 0 && TareaId != 0 && EstadoId == 0 && UsuarioId == "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(t => t.Id.Equals(TareaId))
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId == 0 && TareaId != 0 && EstadoId != 0 && UsuarioId == "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(e => e.EstadoId.Equals(EstadoId))
                    .Where(t => t.Id.Equals(TareaId))
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId == 0 && TareaId != 0 && EstadoId != 0 && UsuarioId != "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(e => e.EstadoId.Equals(EstadoId))
                    .Where(t => t.Id.Equals(TareaId))
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(UsuarioId)))
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId != 0 && TareaId != 0 && EstadoId == 0 && UsuarioId != "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(ProyectoId))
                    .Where(t => t.Id.Equals(TareaId))
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(UsuarioId)))
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId == 0 && TareaId != 0 && EstadoId == 0 && UsuarioId != "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(t => t.Id.Equals(TareaId))
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(UsuarioId)))
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId == 0 && TareaId == 0 && EstadoId != 0 && UsuarioId == "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(e => e.EstadoId.Equals(EstadoId))
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId == 0 && TareaId == 0 && EstadoId != 0 && UsuarioId != "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(e => e.EstadoId.Equals(EstadoId))
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(UsuarioId)))
                    .ToListAsync();
                return result;
            }
            else if (ProyectoId == 0 && TareaId == 0 && EstadoId == 0 && UsuarioId != "0")
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(UsuarioId)))
                    .ToListAsync();
                return result;
            }
            else
            {
                var result = await _context.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .ToListAsync();
                return result;
            }
        }
    }
}
