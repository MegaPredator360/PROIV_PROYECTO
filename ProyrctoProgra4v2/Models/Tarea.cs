using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProyectoProgra4v2.Models;

public class Tarea
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public List<TareaUsuario> TareasUsuarios { get; set; } = null!;

    [ForeignKey("ProyectoId")]
    public int ProyectoId { get; set; }

    [ForeignKey("EstadoId")]
    public int EstadoId { get; set; }
    public Proyecto Proyecto { get; set; } = null!;
    public Estado Estado { get; set; } = null!;
}

public class TareaNew
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la tarea es requerida")]
    [Display(Name = "Nombre")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "La descripcion de la tarea es requerida")]
    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

    [Display(Name = "Estado")]
    [ForeignKey("EstadoId")]
    public int EstadoId { get; set; }

    [Required(ErrorMessage = "No hay proyectos para asignar la tarea")]
    [ForeignKey("ProyectoId")]
    public int ProyectoId { get; set; }

    [Required(ErrorMessage = "Se necesitan asignar Usuarios a la tarea")]
    public List<string> UserIds { get; set; }
}

public class NewTareaDropdowns
{
    public NewTareaDropdowns()
    {
        Estados = new List<Estado>();
        Proyectos = new List<Proyecto>();
        Usuarios = new List<Usuario>();
    }

    public List<Proyecto> Proyectos { get; set; }
    public List<Usuario> Usuarios { get; set; }
    public List<Estado> Estados { get; set; }
}

[Keyless]
public class TareaUsuarioLista
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? NombreEstado { get; set; }

    public string? NombreProyecto { get; set; }
}

[Keyless]
public class TareaLista
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? NombreEstado { get; set; }

    public string? NombreProyecto { get; set; }
    public Int32 PersonasAsignadas { get; set; }

}