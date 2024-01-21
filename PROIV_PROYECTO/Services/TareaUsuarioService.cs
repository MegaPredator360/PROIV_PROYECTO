using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Contexts;
using PROIV_PROYECTO.Interface;
using PROIV_PROYECTO.Models;
using PROIV_PROYECTO.ModelsDTO;

namespace PROIV_PROYECTO.Services
{
    public class TareaUsuarioService : ITareaUsuarioService
    {
        private readonly ProyectoContext proyectoContext;
        private readonly UsuarioContext usuarioContext;
        private readonly UserManager<ApplicationUser> userManager;

        public TareaUsuarioService(ProyectoContext _proyectoContext, UsuarioContext _usuarioContext, UserManager<ApplicationUser> _userManager)
        {
            proyectoContext = _proyectoContext;
            usuarioContext = _usuarioContext;
            userManager = _userManager;
        }
        public async Task<IEnumerable<TareaUsuarioListaDTO>> ObtenerTareaUsuarioAsync(string _filtrar, string _textoBusqueda, string _userName)
        {
            if (_filtrar == "Nombre" && _textoBusqueda != null)
            {
                var result = await proyectoContext.TareaUsuarioListas.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id JOIN TareasUsuarios AS TU ON T.Id = TU.TareaId WHERE TU.UsuarioId = '" + _userName + "' AND T.Nombre LIKE '%" + _textoBusqueda + "%'").ToListAsync();
                return result;
            }
            else if (_filtrar == "Proyecto" && _textoBusqueda != null)
            {
                var result = await proyectoContext.TareaUsuarioListas.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id JOIN TareasUsuarios AS TU ON T.Id = TU.TareaId WHERE TU.UsuarioId = '" + _userName + "' AND P.Nombre LIKE '%" + _textoBusqueda + "%'").ToListAsync();
                return result;
            }
            else if (_filtrar == "Estado" && _textoBusqueda != null)
            {
                var result = await proyectoContext.TareaUsuarioListas.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id JOIN TareasUsuarios AS TU ON T.Id = TU.TareaId WHERE TU.UsuarioId = '" + _userName + "' AND E.NombreEstado LIKE '%" + _textoBusqueda + "%'").ToListAsync();
                return result;
            }
            else
            {
                var result = await proyectoContext.TareaUsuarioListas.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id JOIN TareasUsuarios AS TU ON T.Id = TU.TareaId WHERE TU.UsuarioId = {0}", _userName).ToListAsync();
                return result;
            }
        }

        public async Task ActualizarTareaAsync(int _tareaId, TareaDTO _tareaDTO)
        {
            var tareaEncontrada = await proyectoContext.Tareas.FirstOrDefaultAsync(n => n.Id == _tareaDTO.Id);

            tareaEncontrada.EstadoId = _tareaDTO.EstadoId;
            await proyectoContext.SaveChangesAsync();
        }

    }
}