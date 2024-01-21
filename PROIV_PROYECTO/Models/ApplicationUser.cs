using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROIV_PROYECTO.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? IdNumber { get; set; }
        public string? FullName { get; set; }
    }
}
