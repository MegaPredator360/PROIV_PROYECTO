using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.ModelsDTO
{
    public class UsuarioDropdownDTO
    {
        public UsuarioDropdownDTO()
        {
            Permisos = new List<Permiso>();
        }

        public List<Permiso> Permisos { get; set; }
    }
}