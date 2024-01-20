using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PROIV_PROYECTO.Models
{
    public class Usuario : ApplicationUser
    {
        public List<TareaUsuario> TareasUsuarios { get; set; } = null!;
    }

    public class UsuarioDatos
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "El número de cedula es requerido")]
        public string? IdNumber { get; set; }

        [Required(ErrorMessage = "El nombre completo del usuario es requerido")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "El correo electronico es requerido")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Maximo 6 caracteres y debe contener, 1 mayuscula, 1 minuscula, 1 caracter especial, y 1 número")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Debes de confirmar la contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no son iguales")]
        public string? PasswordConfirm { get; set; }

        public string? Role { get; set; }
    }

    public class NewUsuarioDropdowns
    {
        public NewUsuarioDropdowns()
        {
            appRoles = new List<ApplicationRole>();
        }

        public List<ApplicationRole> appRoles { get; set; }
    }

    public class UsuarioActualizar
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "El número de cedula es requerido")]
        public string? IdNumber { get; set; }

        [Required(ErrorMessage = "El nombre completo del usuario es requerido")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "El correo electronico es requerido")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string? UserName { get; set; }

        public string? Role { get; set; }
    }

    public class UsuarioLista
    {
        public string? Id { get; set; }

        public string? IdNumber { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? UserName { get; set; }

        public string? Role { get; set; }
    }
}
