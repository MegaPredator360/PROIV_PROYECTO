using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.ModelsDTO
{
    public class TareaDropdownDTO
    {
        public TareaDropdownDTO()
        {
            Estados = new List<Estado>();
            Proyectos = new List<Proyecto>();
            Usuarios = new List<Usuario>();
        }

        public List<Proyecto> Proyectos { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public List<Estado> Estados { get; set; }
    }
}