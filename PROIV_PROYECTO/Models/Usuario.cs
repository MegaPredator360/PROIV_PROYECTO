using Microsoft.AspNetCore.Identity;

namespace PROIV_PROYECTO.Models
{
    public class Usuario : IdentityUser
    {
        public string? IdNumber { get; set; }
        public string? FullName { get; set; }
        public List<TareaUsuario> TareasUsuarios { get; set; } = null!;
    }
}
