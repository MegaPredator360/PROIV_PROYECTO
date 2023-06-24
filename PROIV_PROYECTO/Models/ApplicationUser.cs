using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoProgra4v2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? IdNumber { get; set; }
        public string? FullName { get; set; }

    }

    public class ApplicationRole : IdentityRole
    {

    }
}
