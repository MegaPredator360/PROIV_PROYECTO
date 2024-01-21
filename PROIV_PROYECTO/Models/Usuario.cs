using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PROIV_PROYECTO.Models
{
    public class Usuario : ApplicationUser
    {
        public List<TareaUsuario> TareasUsuarios { get; set; } = null!;
    }
}
