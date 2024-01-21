using System.ComponentModel.DataAnnotations;

namespace PROIV_PROYECTO.ModelsDTO
{
    public class IniciarSesionDTO
    {
        [Required(ErrorMessage = "Debes de ingresar el nombre de usuario")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Debes de ingresar la contraseña")]
        public string? Contrasena { get; set; }
    }
}