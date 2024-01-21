using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Contexts;
using PROIV_PROYECTO.Models;
using PROIV_PROYECTO.Interface;
using PROIV_PROYECTO.ModelsDTO;

namespace PROIV_PROYECTO.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly ProyectoContext proyectoContext;

        public ProyectoService(ProyectoContext _proyectoContext)
        {
            proyectoContext = _proyectoContext;
        }

        public async Task NuevoProyectoAsync(ProyectoDTO _proyectoDTO)
        {
            // Se convierten los datos del modelo de ProyectoFormularioDTO a Proyecto
            var nuevoProyecto = new Proyecto()
            {
                Nombre = _proyectoDTO.Nombre,
                Descripcion = _proyectoDTO.Descripcion,
                FechaInicio = _proyectoDTO.FechaInicio,
                EstadoId = _proyectoDTO.EstadoId
            };

            // Se añadirá el proyecto a la tabla
            await proyectoContext.Proyectos.AddAsync(nuevoProyecto);

            // Se guardarán los cambios en la base de datos
            await proyectoContext.SaveChangesAsync();
        }

        public async Task BorrarProyectoAsync(int _proyectoId)
        {
            // Se buscar el proyecto a eliminar
            var projectFound = await proyectoContext.Proyectos.FirstOrDefaultAsync(p => p.Id == _proyectoId);

            // Se elimina el proyecto y las tareas asociadas al proyecto
            proyectoContext.Proyectos.Remove(projectFound!);

            // Se guardan los cambios en la base de datos
            await proyectoContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProyectoListaDTO>> ObtenerProyectosAsync(string _filtrar, string _textoBusqueda)
        {
            string sqlQuery = "SELECT P.Id, P.Nombre, P.FechaInicio, E.NombreEstado, COUNT(T.Id) AS TareasAsignadas FROM Proyectos AS P LEFT JOIN Estados AS E ON P.EstadoId = E.Id LEFT JOIN Tareas AS T ON P.Id = T.ProyectoId ";
            
            // Verificaremos que los campos de filtracion no esten vacios
            if (!string.IsNullOrEmpty(_textoBusqueda) && !string.IsNullOrEmpty(_filtrar))
            {
                if (_filtrar == "Nombre")
                {
                    sqlQuery += "WHERE P.Nombre LIKE '%" + _textoBusqueda + "%' ";
                }
                else if (_filtrar == "Estado")
                {
                    sqlQuery += "WHERE E.NombreEstado LIKE '%" + _textoBusqueda + "%' ";
                }
            }

            sqlQuery += "GROUP BY P.Id, P.Nombre, P.FechaInicio, E.NombreEstado";

            // Mandamos la consulta a la base de datos y esperamos respuesta
            var listaProyecto = await proyectoContext.ProyectoListas
                .FromSqlRaw(sqlQuery)
                .ToListAsync();

            return listaProyecto;
        }

        public async Task<ProyectoDTO> ObtenerProyectoIdAsync(int _proyectoId)
        {
            // Buscamos el proyecto
            var proyectoEncontrado = await proyectoContext.Proyectos
                .Include(e => e.Estado)
                .FirstOrDefaultAsync(p => p.Id == _proyectoId);

            // Lo convertimos a ProyectoDTO
            var proyectoDTO = new ProyectoDTO()
            {
                Id = proyectoEncontrado.Id,
                Nombre = proyectoEncontrado.Nombre,
                Descripcion = proyectoEncontrado.Descripcion,
                FechaInicio = proyectoEncontrado.FechaInicio,
                EstadoId = proyectoEncontrado.EstadoId
            };

            return proyectoDTO;
        }

        public async Task<IList<ProyectoDetalleDTO>> ObtenerProyectoDetalleAsync(int _proyectoId)
        {
            var result = await proyectoContext.ProyectoDetalles.FromSqlRaw("SELECT P.Id, P.Nombre, P.Descripcion, P.FechaInicio, PE.NombreEstado AS ProyectoEstado, T.Id as IdTarea, T.Nombre as TareaNombre, E.NombreEstado AS TareaEstado, COUNT(UT.UsuarioId) AS PersonasAsignadas FROM Proyectos AS P JOIN Tareas AS T ON P.Id = T.ProyectoId JOIN Estados AS PE ON P.EstadoId = PE.Id JOIN Estados AS E ON T.EstadoId = E.Id JOIN TareasUsuarios AS UT ON T.Id = UT.TareaId WHERE P.Id = " + _proyectoId + " GROUP BY P.Id, P.Nombre, P.Descripcion, P.FechaInicio, PE.NombreEstado, T.Id, T.Nombre, E.NombreEstado").ToListAsync();
            return result;
        }

        public async Task ActualizarProyectoAsync(int _proyectoId, ProyectoDTO _proyectoDTO)
        {
            var dbProyecto = await proyectoContext.Proyectos.FirstOrDefaultAsync(n => n.Id == _proyectoDTO.Id);

            if (dbProyecto != null)
            {
                dbProyecto.Id = _proyectoDTO.Id;
                dbProyecto.Nombre = _proyectoDTO.Nombre;
                dbProyecto.Descripcion = _proyectoDTO.Descripcion;
                dbProyecto.FechaInicio = _proyectoDTO.FechaInicio;
                dbProyecto.EstadoId = _proyectoDTO.EstadoId;

                await proyectoContext.SaveChangesAsync();
            }
        }

        public async Task<ProyectoDropdownDTO> ProyectoDropdownValues()
        {
            var response = new ProyectoDropdownDTO()
            {
                Estados = await proyectoContext.Estados.OrderBy(n => n.NombreEstado).ToListAsync()
            };

            return response;
        }
    }
}
