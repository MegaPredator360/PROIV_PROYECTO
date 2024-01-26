using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Models;
using System.Reflection.Emit;

namespace PROIV_PROYECTO.Contexts
{
    public class ProyectoContext : DbContext
    {
        public ProyectoContext(DbContextOptions<ProyectoContext> options) : base(options) { }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<TareaUsuario> TareasUsuarios { get; set; }
        public DbSet<Estado> Estados { get; set; }

        protected override void OnModelCreating(ModelBuilder _builder)
        {
            _builder.Entity<Usuario>().ToTable("AspNetUsers");

            // Se crea la tabla relacional entre Tareas y Usuarios
            _builder.Entity<TareaUsuario>().HasKey(ut => new
            {
                ut.UsuarioId,
                ut.TareaId
            });

            // Indicamos que la Foreign Key de Tarea a Proyecto no haga nada
            _builder.Entity<Tarea>().HasOne(t => t.Proyecto).WithMany(p => p.Tareas).HasForeignKey(t => t.ProyectoId).OnDelete(DeleteBehavior.NoAction);

            // Declaramos las llaves primarias de la tabla relacional
            _builder.Entity<TareaUsuario>().HasOne(u => u.Usuario).WithMany(ut => ut.TareasUsuarios).HasForeignKey(u => u.UsuarioId);
            _builder.Entity<TareaUsuario>().HasOne(t => t.Tarea).WithMany(ut => ut.TareasUsuarios).HasForeignKey(t => t.TareaId);

            // Anidamos la tabla de Estado con los siguientes datos
            _builder.Entity<Estado>().HasData( new Estado { Id = 1, NombreEstado = "Completado" });
            _builder.Entity<Estado>().HasData( new Estado { Id = 2, NombreEstado = "Cancelado" });
            _builder.Entity<Estado>().HasData( new Estado { Id = 3, NombreEstado = "Retrasado" });
            _builder.Entity<Estado>().HasData( new Estado { Id = 4, NombreEstado = "En Proceso" });

            base.OnModelCreating(_builder);
        }
    }
}
