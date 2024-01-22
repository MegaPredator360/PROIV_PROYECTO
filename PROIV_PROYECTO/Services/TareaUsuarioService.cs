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
        private readonly UserManager<ApplicationUser> userManager;

        public TareaUsuarioService(ProyectoContext _proyectoContext, UserManager<ApplicationUser> _userManager)
        {
            proyectoContext = _proyectoContext;
            userManager = _userManager;
        }
        public async Task<IEnumerable<TareaUsuarioListaDTO>> ObtenerTareaUsuarioAsync(string _filtrar, string _textoBusqueda, string _userName)
        {
            if (_filtrar == "Nombre" && _textoBusqueda != null)
            {
                var result = await proyectoContext.TareasUsuarios.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id JOIN TareasUsuarios AS TU ON T.Id = TU.TareaId WHERE TU.UsuarioId = '" + _userName + "' AND T.Nombre LIKE '%" + _textoBusqueda + "%'").ToListAsync();
                IEnumerable<TareaUsuarioListaDTO> tareaListaDTOs = result.ConvertAll(x => (TareaUsuarioListaDTO)x);

                return tareaListaDTOs;
            }
            else if (_filtrar == "Proyecto" && _textoBusqueda != null)
            {
                var result = await proyectoContext.TareasUsuarios.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id JOIN TareasUsuarios AS TU ON T.Id = TU.TareaId WHERE TU.UsuarioId = '" + _userName + "' AND P.Nombre LIKE '%" + _textoBusqueda + "%'").ToListAsync();
                IEnumerable<TareaUsuarioListaDTO> tareaListaDTOs = result.ConvertAll(x => (TareaUsuarioListaDTO)x);

                return tareaListaDTOs;
            }
            else if (_filtrar == "Estado" && _textoBusqueda != null)
            {
                var result = await proyectoContext.TareasUsuarios.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id JOIN TareasUsuarios AS TU ON T.Id = TU.TareaId WHERE TU.UsuarioId = '" + _userName + "' AND E.NombreEstado LIKE '%" + _textoBusqueda + "%'").ToListAsync();
                IEnumerable<TareaUsuarioListaDTO> tareaListaDTOs = result.ConvertAll(x => (TareaUsuarioListaDTO)x);

                return tareaListaDTOs;
            }
            else
            {
                var result = await proyectoContext.TareasUsuarios.FromSqlRaw("SELECT T.Id, T.Nombre, P.Nombre AS NombreProyecto, E.NombreEstado FROM TAREAS AS T LEFT JOIN Proyectos AS P ON T.ProyectoId = P.Id LEFT JOIN Estados AS E ON T.EstadoId = E.Id JOIN TareasUsuarios AS TU ON T.Id = TU.TareaId WHERE TU.UsuarioId = {0}", _userName).ToListAsync();
                IEnumerable<TareaUsuarioListaDTO> tareaListaDTOs = result.ConvertAll(x => (TareaUsuarioListaDTO)x);

                return tareaListaDTOs;
            }
        }

        public async Task ActualizarTareaAsync(int _tareaId, TareaDTO _tareaDTO)
        {
            var tareaEncontrada = await proyectoContext.Tareas.FirstOrDefaultAsync(n => n.Id == _tareaDTO.Id);

            tareaEncontrada!.EstadoId = _tareaDTO.EstadoId;
            await proyectoContext.SaveChangesAsync();
        }

    }
}