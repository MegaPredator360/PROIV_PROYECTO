using System.ComponentModel.DataAnnotations;

namespace ProyectoProgra4v2.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Debes de ingresar el nombre de usuario")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Debes de ingresar la contraseña")]
        public string? Password { get; set; }
    }

    public class Status
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }

    public class ChangePassword
    {
        [Required(ErrorMessage = "Debes de ingresar la contraseña actual")]
        public string? CurrentPassword { get; set; }

        [Required(ErrorMessage = "Debes de ingresar la nueva contraseña")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Maximo 6 caracteres, debe contener: 1 mayuscula, 1 minuscula, 1 caracter especial, y 1 número")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Debes de confirmar la nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no son iguales")]
        public string? PasswordConfirm { get; set; }

    }
}
