using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Contexts;
using PROIV_PROYECTO.Models;
using PROIV_PROYECTO.Interface;
using PROIV_PROYECTO.ModelsDTO.ProyectoDTO;
using System.Collections.Generic;
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

        public async Task NuevoProyectoAsync(ProyectoFormularioDTO _proyectoDTO)
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

        public async Task<IEnumerable<ProyectoListaDTO>> ObtenerProyectosAsync(string _nombreProyecto, int _estadoId)
        {
            IEnumerable<Proyecto> listaProyectos = await proyectoContext.Proyectos
                .Include(e => e.Estado)
                // Filtrará por nombre de proyecto
                .Where(p => (_nombreProyecto == null ? true : p.Nombre!.Contains(_nombreProyecto)))
                // Si _estadoId es igual a 0, el filtro es ignorado, pero si es diferennte de 0, filtrará por estado
                .Where(p => (_estadoId == 0 ? true : p.EstadoId == _estadoId))
                .ToListAsync();

            IEnumerable<ProyectoListaDTO> proyectosDTO = listaProyectos.Select(p => new ProyectoListaDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                FechaInicio = p.FechaInicio,
                EstadoId = p.EstadoId,
                EstadoDTOs = new EstadoDTO
                {
                    Id = p.Estado.Id,
                    NombreEstado = p.Estado.NombreEstado
                },
                TareasAsignadas = 0
            }).ToList();

            return proyectosDTO;
        }

        public async Task<ProyectoFormularioDTO> ObtenerProyectoIdAsync(int _proyectoId)
        {
            // Buscamos el proyecto
            var proyectoEncontrado = await proyectoContext.Proyectos
                .Include(e => e.Estado)
                .FirstOrDefaultAsync(p => p.Id == _proyectoId);

            // Si el proyecto regresa nulo, retornaremos un dato nulo para que en la interface cargue una pantalla de error
            if (proyectoEncontrado == null)
            {
                return null!;
            }

            var proyectoDTO = new ProyectoFormularioDTO()
            {
                Id = proyectoEncontrado!.Id,
                Nombre = proyectoEncontrado.Nombre,
                Descripcion = proyectoEncontrado.Descripcion,
                FechaInicio = proyectoEncontrado.FechaInicio,
                EstadoId = proyectoEncontrado.EstadoId
            };

            return proyectoDTO;
        }

        public async Task<ProyectoDetalleDTO> ObtenerProyectoDetalleAsync(int _proyectoId)
        {
            // Buscamos el proyecto
            var proyectoEncontrado = await proyectoContext.Proyectos
                .Include(e => e.Estado)
                .FirstOrDefaultAsync(p => p.Id == _proyectoId);

            if (proyectoEncontrado == null)
            {
                return null!;
            }

            IEnumerable<Tarea> listaTareas = await proyectoContext.Tareas.Where(p => p.ProyectoId == _proyectoId).ToListAsync();

            IEnumerable<TareaListaDTO> tareaDTO = listaTareas.Select(p => new TareaListaDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                NombreEstado = p.EstadoId.ToString(),
                NombreProyecto = "N/A",
                PersonasAsignadas = 0
            }).ToList();

            // Lo convertimos a ProyectoDTO
            var proyectoDTO = new ProyectoDetalleDTO()
            {
                Id = proyectoEncontrado!.Id,
                Nombre = proyectoEncontrado.Nombre,
                Descripcion = proyectoEncontrado.Descripcion,
                FechaInicio = proyectoEncontrado.FechaInicio,
                EstadoId = proyectoEncontrado.EstadoId,
                EstadoDTOs = new EstadoDTO
                {
                    Id = proyectoEncontrado.Estado.Id,
                    NombreEstado = proyectoEncontrado.Estado.NombreEstado
                },
                Tareas = tareaDTO
            };

            return proyectoDTO;
        }

        public async Task ActualizarProyectoAsync(int _proyectoId, ProyectoFormularioDTO _proyectoDTO)
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
