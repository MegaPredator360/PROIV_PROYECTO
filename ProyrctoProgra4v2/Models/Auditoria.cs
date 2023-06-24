using Microsoft.EntityFrameworkCore;

namespace ProyectoProgra4v2.Models
{
    [Keyless]
    public class Auditoria : Tarea
    {
        
    }

    public class NewAuditoriaDropdowns
    {
        public NewAuditoriaDropdowns()
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
