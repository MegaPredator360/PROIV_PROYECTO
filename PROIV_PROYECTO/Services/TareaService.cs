using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Models;
using PROIV_PROYECTO.Interface;
using PROIV_PROYECTO.Contexts;
using PROIV_PROYECTO.ModelsDTO;

namespace PROIV_PROYECTO.Services
{
    public class TareaService : ITareaService
    {
        private readonly ProyectoContext proyectoContext;

        public TareaService(ProyectoContext _proyectoContext)
        {
            proyectoContext = _proyectoContext;
        }

        public async Task NuevaTareaAsync(TareaDTO _tareaDTO)
        {
            var tareaNueva = new Tarea()
            {
                Nombre = _tareaDTO.Nombre,
                Descripcion = _tareaDTO.Descripcion,
                ProyectoId = _tareaDTO.ProyectoId,
                EstadoId = _tareaDTO.EstadoId
            };

            await proyectoContext.Tareas.AddAsync(tareaNueva);
            await proyectoContext.SaveChangesAsync();

            //Se añadiran los usuarios asignados a la tabla relacional de TareaUsuario
            foreach (var usuarioId in _tareaDTO.AssignedUsersId!)
            {
                var nuevaTareaUsuario = new TareaUsuario()
                {
                    TareaId = _tareaDTO.Id,
                    UsuarioId = usuarioId
                };
                await proyectoContext.TareasUsuarios.AddAsync(nuevaTareaUsuario);
            }
            await proyectoContext.SaveChangesAsync();
        }

        public async Task BorrarTareaAsync(int Id)
        {
            // Buscamos la tarea
            var tareaEncontrada = await proyectoContext.Tareas.FirstOrDefaultAsync(p => p.Id == Id);

            // Removemos la tarea
            proyectoContext.Tareas.Remove(tareaEncontrada!);

            // Guardamos los cambios en la base de datos
            await proyectoContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TareaListaDTO>> ObtenerTareaAsync(string _filtrar, string _textoBusqueda)
        {
            IEnumerable<Tarea> listaTareas = await proyectoContext.Tareas.ToListAsync();

            IEnumerable<TareaListaDTO> tareaDTO = listaTareas.Select(p => new TareaListaDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                NombreEstado = p.EstadoId.ToString(),
                NombreProyecto = "N/A",
                PersonasAsignadas = 0
            }).ToList();

            return tareaDTO;
        }

        public async Task<TareaDTO> ObtenerTareaIdAsync(int _tareaId)
        {

            var tareaEncontrada = await proyectoContext.Tareas
                .Include(p => p.Proyecto)
                .Include(e => e.Estado)
                .FirstOrDefaultAsync(t => t.Id == _tareaId);

            var obtenerUsuarios = await proyectoContext.TareasUsuarios
                .FirstOrDefaultAsync(tu => tu.TareaId == _tareaId);

            List<string>? usuarioAsignados = null;

            foreach (var usuarioId in obtenerUsuarios!.UsuarioId!)
            {
                usuarioAsignados!.Add(usuarioId.ToString());
            }

            var tareaDTO = new TareaDTO()
            {
                Id = tareaEncontrada!.Id,
                Nombre = tareaEncontrada.Nombre,
                Descripcion = tareaEncontrada.Descripcion,
                EstadoId = tareaEncontrada.EstadoId,
                ProyectoId = tareaEncontrada.ProyectoId,
                AssignedUsersId = usuarioAsignados
            };

            return tareaDTO;
        }

        public async Task ActualizarTareaAsync(int _tareaId, TareaDTO _tareaDTO)
        {
            var tareaEncontrada = await proyectoContext.Tareas.FirstOrDefaultAsync(n => n.Id == _tareaDTO.Id);

            if (tareaEncontrada != null)
            {
                tareaEncontrada.Id = _tareaDTO.Id;
                tareaEncontrada.Nombre = _tareaDTO.Nombre;
                tareaEncontrada.Descripcion = _tareaDTO.Descripcion;
                tareaEncontrada.ProyectoId = _tareaDTO.ProyectoId;
                tareaEncontrada.EstadoId = _tareaDTO.EstadoId;
                await proyectoContext.SaveChangesAsync();
            }

            // Se realizara una lista de los usuario asignados a la tarea a actualizar
            var usuariosAsignados = proyectoContext.TareasUsuarios.Where(ut => ut.TareaId == _tareaDTO.Id).ToList();

            // Se eliminarán todos los usuarios asignados de la tabla relacional
            proyectoContext.TareasUsuarios.RemoveRange(usuariosAsignados);
            await proyectoContext.SaveChangesAsync();

            // Se agregarán los usuarios asignados a la tabla relacional
            foreach (var usuarioId in _tareaDTO.AssignedUsersId!)
            {
                var nuevaTareaUsuario = new TareaUsuario()
                {
                    TareaId = _tareaDTO.Id,
                    UsuarioId = usuarioId
                };
                await proyectoContext.TareasUsuarios.AddAsync(nuevaTareaUsuario);
            }
            await proyectoContext.SaveChangesAsync();
        }

        public async Task<TareaDropdownDTO> TareaDropdownValues()
        {
            var response = new TareaDropdownDTO()
            {
                Proyectos = await proyectoContext.Proyectos.OrderBy(n => n.Nombre).ToListAsync(),
                Estados = await proyectoContext.Estados.OrderBy(n => n.NombreEstado).ToListAsync(),
                Usuarios = await proyectoContext.Usuarios.OrderBy(n => n.FullName).ToListAsync(),
            };

            return response;
        }
    }
}
