using System.ComponentModel.DataAnnotations;

namespace PROIV_PROYECTO.ModelsDTO
{
    public class CambiarContrasenaDTO
    {
        [Required(ErrorMessage = "Debes de ingresar la contraseña actual")]
        public string? ContrasenaActual { get; set; }

        [Required(ErrorMessage = "Debes de ingresar la nueva contraseña")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Maximo 6 caracteres, debe contener: 1 mayuscula, 1 minuscula, 1 caracter especial, y 1 número")]
        public string? ContrasenaNueva { get; set; }

        [Required(ErrorMessage = "Debes de confirmar la nueva contraseña")]
        [Compare("ContrasenaNueva", ErrorMessage = "Las contraseñas no son iguales")]
        public string? ContrasenaConfirmar { get; set; }
    }
}