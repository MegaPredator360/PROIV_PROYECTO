using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.ModelsDTO
{
    public class ProyectoDropdownDTO
    {
        public ProyectoDropdownDTO()
        {
            Estados = new List<Estado>();
        }
        public List<Estado> Estados { get; set; }
    }
}