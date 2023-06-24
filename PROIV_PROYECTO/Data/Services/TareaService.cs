using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ProyectoProgra4v2.Models;
using ProyectoProgra4v2.Data.Services.Interface;

namespace ProyectoProgra4v2.Data.Services
{
    public class TareaService : ITareaService
    {
        private readonly MvcDbContext _context;
        private readonly ApplicationDbContext _appContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public TareaService(MvcDbContext context, ApplicationDbContext appContext, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _appContext = appContext;
            _userManager = userManager;
        }

        public async Task AddAsync(TareaNew tareaNew)
        {
            var newTarea = new Tarea()
            {
                Nombre = tareaNew.Nombre,
                Descripcion = tareaNew.Descripcion,
                ProyectoId = tareaNew.ProyectoId,
                EstadoId = tareaNew.EstadoId
            };

            await _context.Tareas.AddAsync(newTarea);
            await _context.SaveChangesAsync();

            //Añadir a la tabla relacional
            foreach (var userId in tareaNew.UserIds)
            {
                var newTareaUsuario = new TareaUsuario()
                {
                    TareaId = newTarea.Id,
                    UsuarioId = userId
                };
                await _context.TareasUsuarios.AddAsync(newTareaUsuario);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var result = await _context.Tareas.FirstOrDefaultAsync(p => p.Id == Id);
            _context.Tareas.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TareaLista>> GetAllAsync(string SearchBy, string SearchString)
        {
            if (SearchBy == "Nombre")
            {
                var result = await _context.TareaListas.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado, COUNT(UT.UsuarioId) AS PersonasAsignadas FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id LEFT JOIN TareasUsuarios AS UT ON T.Id = UT.TareaId WHERE T.Nombre LIKE '%" + SearchString + "%' GROUP BY T.Id, T.Nombre, P.Nombre, E.NombreEstado").ToListAsync();
                return result;
            }
            else if (SearchBy == "Proyecto")
            {
                var result = await _context.TareaListas.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado, COUNT(UT.UsuarioId) AS PersonasAsignadas FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id LEFT JOIN TareasUsuarios AS UT ON T.Id = UT.TareaId WHERE P.Nombre LIKE '%" + SearchString + "%' GROUP BY T.Id, T.Nombre, P.Nombre, E.NombreEstado").ToListAsync();
                return result;
            }
            else if (SearchBy == "Estado")
            {
                var result = await _context.TareaListas.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado, COUNT(UT.UsuarioId) AS PersonasAsignadas FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id LEFT JOIN TareasUsuarios AS UT ON T.Id = UT.TareaId WHERE E.NombreEstado LIKE '%" + SearchString + "%' GROUP BY T.Id, T.Nombre, P.Nombre, E.NombreEstado").ToListAsync();
                return result;
            }
            else
            {
                var result = await _context.TareaListas.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado, COUNT(UT.UsuarioId) AS PersonasAsignadas FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id LEFT JOIN TareasUsuarios AS UT ON T.Id = UT.TareaId GROUP BY T.Id, T.Nombre, P.Nombre, E.NombreEstado").ToListAsync();
                return result;
            }
        }

        public async Task<IEnumerable<TareaUsuarioLista>> GetAllUserAsync(string SearchBy, string SearchString, string username)
        {

            if (SearchBy == "Nombre" && SearchString != null)
            {
                var result = await _context.TareaUsuarioListas.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id JOIN TareasUsuarios AS TU ON T.Id = TU.TareaId WHERE TU.UsuarioId = '" + username + "' AND T.Nombre LIKE '%" + SearchString + "%'").ToListAsync();
                return result;
            }
            else if (SearchBy == "Proyecto" && SearchString != null)
            {
                var result = await _context.TareaUsuarioListas.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id JOIN TareasUsuarios AS TU ON T.Id = TU.TareaId WHERE TU.UsuarioId = '" + username + "' AND P.Nombre LIKE '%" + SearchString + "%'").ToListAsync();
                return result;
            }
            else if (SearchBy == "Estado" && SearchString != null)
            {
                var result = await _context.TareaUsuarioListas.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id JOIN TareasUsuarios AS TU ON T.Id = TU.TareaId WHERE TU.UsuarioId = '" + username + "' AND E.NombreEstado LIKE '%" + SearchString + "%'").ToListAsync();
                return result;
            }
            else
            {
                var result = await _context.TareaUsuarioListas.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id JOIN TareasUsuarios AS TU ON T.Id = TU.TareaId WHERE TU.UsuarioId = {0}", username).ToListAsync();
                return result;
            }
        }

        public async Task<Tarea> GetByIdAsync(int Id)
        {

            var result = await _context.Tareas
                .Include(p => p.Proyecto)
                .Include(e => e.Estado)
                .Include(ut => ut.TareasUsuarios).ThenInclude(u => u.Usuario)
                .FirstOrDefaultAsync(t => t.Id == Id);
            return result;
        }

        public async Task UpdateAsync(int Id, TareaNew tareaNew)
        {
            var dbTarea = await _context.Tareas.FirstOrDefaultAsync(n => n.Id == tareaNew.Id);

            if (dbTarea != null)
            {
                dbTarea.Id = tareaNew.Id;
                dbTarea.Nombre = tareaNew.Nombre;
                dbTarea.Descripcion = tareaNew.Descripcion;
                dbTarea.ProyectoId = tareaNew.ProyectoId;
                dbTarea.EstadoId = tareaNew.EstadoId;
                await _context.SaveChangesAsync();
            }

            var usuarioExistenteDb = _context.TareasUsuarios.Where(ut => ut.TareaId == tareaNew.Id).ToList();
            _context.TareasUsuarios.RemoveRange(usuarioExistenteDb);
            await _context.SaveChangesAsync();

            foreach (var usuarioId in tareaNew.UserIds)
            {
                var newTareaUsuario = new TareaUsuario()
                {
                    TareaId = tareaNew.Id,
                    UsuarioId = usuarioId
                };
                await _context.TareasUsuarios.AddAsync(newTareaUsuario);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<NewTareaDropdowns> GetNewTareaDropdownsValues()
        {
            var response = new NewTareaDropdowns()
            {
                Proyectos = await _context.Proyectos.OrderBy(n => n.Nombre).ToListAsync(),
                Estados = await _context.Estados.OrderBy(n => n.NombreEstado).ToListAsync(),
                Usuarios = await _context.Usuarios.OrderBy(n => n.FullName).ToListAsync(),
            };

            return response;
        }

        public async Task UpdateUsuarioAsync(int Id, Tarea tarea)
        {
            var dbTarea = await _context.Tareas.FirstOrDefaultAsync(n => n.Id == tarea.Id);

            dbTarea.EstadoId = tarea.EstadoId;
            await _context.SaveChangesAsync();
        }
    }
}
