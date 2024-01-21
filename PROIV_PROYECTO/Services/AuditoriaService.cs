using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Contexts;
using PROIV_PROYECTO.Interface;
using PROIV_PROYECTO.Models;
using PROIV_PROYECTO.ModelsDTO;

namespace PROIV_PROYECTO.Services
{
    public class AuditoriaService : IAuditoriaService
    {

        // Creamos una variable que hará uso de contexto
        private readonly ProyectoContext proyectoContext;

        // Constructor
        public AuditoriaService(ProyectoContext _proyectoContext)
        {
            proyectoContext = _proyectoContext;
        }

        // Desplegables que se usaran para filtraciones en la auditoria
        public async Task<AuditoriaDropdownsDTO> AuditoriaDropdownValues()
        {
            var response = new AuditoriaDropdownsDTO()
            {
                Proyectos = await proyectoContext.Proyectos.OrderBy(n => n.Nombre).ToListAsync(),
                Tareas = await proyectoContext.Tareas.OrderBy(n => n.Nombre).ToListAsync(),
                Estados = await proyectoContext.Estados.OrderBy(n => n.NombreEstado).ToListAsync(),
                Usuarios = await proyectoContext.Usuarios.OrderBy(n => n.FullName).ToListAsync(),
            };

            return response;
        }

        // Obtendremos todas las tareas y proyectos
        /*
        public async Task<IEnumerable<Tarea>> ObtenerProyectosTareasAsync(int _proyectoId, int _tareaId, int _estadoId, string _usuarioId)
        {
            if (_proyectoId == 0 && _tareaId == 0 && _estadoId == 0 && _usuarioId == null)
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId != 0 && _tareaId == 0 && _estadoId == 0 && _usuarioId == "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(_proyectoId))
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId != 0 && _tareaId != 0 && _estadoId == 0 && _usuarioId == "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(_proyectoId))
                    .Where(t => t.Id.Equals(_tareaId))
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId != 0 && _tareaId == 0 && _estadoId != 0 && _usuarioId == "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(_proyectoId))
                    .Where(e => e.EstadoId.Equals(_estadoId))
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId != 0 && _tareaId == 0 && _estadoId == 0 && _usuarioId != "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(_proyectoId))
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(_usuarioId)))
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId != 0 && _tareaId != 0 && _estadoId != 0 && _usuarioId == "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(_proyectoId))
                    .Where(t => t.Id.Equals(_tareaId))
                    .Where(e => e.EstadoId.Equals(_estadoId))
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId != 0 && _tareaId == 0 && _estadoId != 0 && _usuarioId != "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(_proyectoId))
                    .Where(e => e.EstadoId.Equals(_estadoId))
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(_usuarioId)))
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId != 0 && _tareaId != 0 && _estadoId != 0 && _usuarioId != "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(_proyectoId))
                    .Where(t => t.Id.Equals(_tareaId))
                    .Where(e => e.EstadoId.Equals(_estadoId))
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(_usuarioId)))
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId == 0 && _tareaId != 0 && _estadoId == 0 && _usuarioId == "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(t => t.Id.Equals(_tareaId))
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId == 0 && _tareaId != 0 && _estadoId != 0 && _usuarioId == "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(e => e.EstadoId.Equals(_estadoId))
                    .Where(t => t.Id.Equals(_tareaId))
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId == 0 && _tareaId != 0 && _estadoId != 0 && _usuarioId != "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(e => e.EstadoId.Equals(_estadoId))
                    .Where(t => t.Id.Equals(_tareaId))
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(_usuarioId)))
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId != 0 && _tareaId != 0 && _estadoId == 0 && _usuarioId != "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(p => p.ProyectoId.Equals(_proyectoId))
                    .Where(t => t.Id.Equals(_tareaId))
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(_usuarioId)))
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId == 0 && _tareaId != 0 && _estadoId == 0 && _usuarioId != "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(t => t.Id.Equals(_tareaId))
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(_usuarioId)))
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId == 0 && _tareaId == 0 && _estadoId != 0 && _usuarioId == "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(e => e.EstadoId.Equals(_estadoId))
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId == 0 && _tareaId == 0 && _estadoId != 0 && _usuarioId != "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(e => e.EstadoId.Equals(_estadoId))
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(_usuarioId)))
                    .ToListAsync();
                return result;
            }
            else if (_proyectoId == 0 && _tareaId == 0 && _estadoId == 0 && _usuarioId != "0")
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId.Equals(_usuarioId)))
                    .ToListAsync();
                return result;
            }
            else
            {
                var result = await projectsContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(e => e.Estado)
                    .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                    .ToListAsync();
                return result;
            }
        }
        */

        public async Task<IEnumerable<Tarea>> ObtenerAuditoriaAsync(int _proyectoId, int _tareaId, int _estadoId, string _usuarioId)
        {
            var query = proyectoContext.Tareas.AsQueryable();

            if (_proyectoId != 0)
            {
                query = query.Where(p => p.ProyectoId == _proyectoId);
            }

            if (_tareaId != 0)
            {
                query = query.Where(t => t.Id == _tareaId);
            }

            if (_estadoId != 0)
            {
                query = query.Where(e => e.EstadoId == _estadoId);
            }

            if (!string.IsNullOrEmpty(_usuarioId) && _usuarioId != "0")
            {
                query = query.Where(ut => ut.TareasUsuarios.Any(u => u.UsuarioId == _usuarioId));
            }

            var result = await query
                .Include(p => p.Proyecto)
                .Include(e => e.Estado)
                .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                .ToListAsync();

            return result;
        }
    }
}
