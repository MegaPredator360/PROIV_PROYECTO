using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.ModelsDTO
{
    public class AuditoriaDropdownsDTO
    {
        public AuditoriaDropdownsDTO()
        {
            Proyectos = new List<Proyecto>();
            Tareas = new List<Tarea>();
            Estados = new List<Estado>();
            Usuarios = new List<Usuario>();
        }
        public List<Proyecto> Proyectos { get; set; }
        public List<Tarea> Tareas { get; set; }
        public List<Estado> Estados { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}