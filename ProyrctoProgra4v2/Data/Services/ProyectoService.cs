using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using ProyectoProgra4v2.Models;
using ProyectoProgra4v2.Data.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using System.Threading;

namespace ProyectoProgra4v2.Data.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly MvcDbContext _context;

        public ProyectoService(MvcDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProyectoNew proyectoNew)
        {
            var newProyecto = new Proyecto()
            {
                Nombre = proyectoNew.Nombre,
                Descripcion = proyectoNew.Descripcion,
                FechaInicio = proyectoNew.FechaInicio,
                EstadoId = proyectoNew.EstadoId
            };

            await _context.Proyectos.AddAsync(newProyecto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var result = await _context.Proyectos.FirstOrDefaultAsync(p => p.Id == Id);
            _context.Proyectos.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProyectoLista>> GetAllAsync(string SearchBy, string SearchString)
        {
            if (SearchBy == "Nombre")
            {
                var result = await _context.ProyectoListas.FromSqlRaw("SELECT P.Id, P.Nombre, P.FechaInicio, E.NombreEstado, COUNT(T.Id) AS TareasAsignadas FROM Proyectos AS P LEFT JOIN Estados AS E ON P.EstadoId = E.Id LEFT JOIN Tareas AS T ON P.Id = T.ProyectoId WHERE P.Nombre LIKE '%" + SearchString + "%' GROUP BY P.Id, P.Nombre, P.FechaInicio, E.NombreEstado").ToListAsync();
                return result;
            }
            else if (SearchBy == "Estado")
            {
                var result = await _context.ProyectoListas.FromSqlRaw("SELECT P.Id, P.Nombre, P.FechaInicio, E.NombreEstado, COUNT(T.Id) AS TareasAsignadas FROM Proyectos AS P LEFT JOIN Estados AS E ON P.EstadoId = E.Id LEFT JOIN Tareas AS T ON P.Id = T.ProyectoId WHERE E.NombreEstado LIKE '%" + SearchString + "%' GROUP BY P.Id, P.Nombre, P.FechaInicio, E.NombreEstado").ToListAsync();
                return result;
            }
            else
            {
                var result = await _context.ProyectoListas.FromSqlRaw("SELECT P.Id, P.Nombre, P.FechaInicio, E.NombreEstado, COUNT(T.Id) AS TareasAsignadas FROM Proyectos AS P LEFT JOIN Estados AS E ON P.EstadoId = E.Id LEFT JOIN Tareas AS T ON P.Id = T.ProyectoId GROUP BY P.Id, P.Nombre, P.FechaInicio, E.NombreEstado").ToListAsync();
                return result;
            }
        }

        public async Task<Proyecto> GetByIdAsync(int Id)
        {
            var result = await _context.Proyectos
                .Include(e => e.Estado)
                .FirstOrDefaultAsync(p => p.Id == Id);
            return result;
        }

        public async Task<IList<ProyectoDetalle>> GetByIdDetalleAsync(int Id)
        {
            var result = await _context.ProyectoDetalles.FromSqlRaw("SELECT P.Id, P.Nombre, P.Descripcion, P.FechaInicio, PE.NombreEstado AS ProyectoEstado, T.Id as IdTarea, T.Nombre as TareaNombre, E.NombreEstado AS TareaEstado, COUNT(UT.UsuarioId) AS PersonasAsignadas FROM Proyectos AS P JOIN Tareas AS T ON P.Id = T.ProyectoId JOIN Estados AS PE ON P.EstadoId = PE.Id JOIN Estados AS E ON T.EstadoId = E.Id JOIN TareasUsuarios AS UT ON T.Id = UT.TareaId WHERE P.Id = " + Id + " GROUP BY P.Id, P.Nombre, P.Descripcion, P.FechaInicio, PE.NombreEstado, T.Id, T.Nombre, E.NombreEstado").ToListAsync();
            return result;
        }

        public async Task UpdateAsync(int Id, ProyectoNew proyectoNew)
        {
            var dbProyecto = await _context.Proyectos.FirstOrDefaultAsync(n => n.Id == proyectoNew.Id);

            if (dbProyecto != null)
            {
                dbProyecto.Id = proyectoNew.Id;
                dbProyecto.Nombre = proyectoNew.Nombre;
                dbProyecto.Descripcion = proyectoNew.Descripcion;
                dbProyecto.FechaInicio = proyectoNew.FechaInicio;
                dbProyecto.EstadoId = proyectoNew.EstadoId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<NewProyectoDropdowns> GetNewProyectoDropdownsValues()
        {
            var response = new NewProyectoDropdowns()
            {
                Estados = await _context.Estados.OrderBy(n => n.NombreEstado).ToListAsync()
            };

            return response;
        }
    }
}
