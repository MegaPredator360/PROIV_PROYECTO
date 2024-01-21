using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Models;
using static System.Net.Mime.MediaTypeNames;

namespace PROIV_PROYECTO.Contexts
{
    public class ProyectoContext : DbContext
    {
        public ProyectoContext(DbContextOptions<ProyectoContext> options) : base(options) { }

        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<TareaUsuario> TareasUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<TareaLista> TareaListas { get; set; }
        public DbSet<TareaUsuarioLista> TareaUsuarioListas { get; set; }
        public DbSet<ProyectoLista> ProyectoListas { get; set; }
        public DbSet<ProyectoDetalle> ProyectoDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder _builder)
        {
            // Se crea la tabla relacional entre Tareas y Usuarios
            _builder.Entity<TareaUsuario>().HasKey(ut => new
            {
                ut.UsuarioId,
                ut.TareaId
            });

            // Declaramos las llaves primarias de la tabla relacional
            _builder.Entity<TareaUsuario>().HasOne(u => u.Usuario).WithMany(ut => ut.TareasUsuarios).HasForeignKey(u => u.UsuarioId);
            _builder.Entity<TareaUsuario>().HasOne(t => t.Tarea).WithMany(ut => ut.TareasUsuarios).HasForeignKey(t => t.TareaId);

            // Indicamos que se ignoren estos modelos ya que solo son usado para mover informacion del Frontend and Backend
            _builder.Ignore<TareaNew>();
            _builder.Ignore<NewTareaDropdowns>();
            _builder.Ignore<NewProyectoDropdowns>();
            _builder.Ignore<ProyectoNew>();

            // Indicamos que el modelo usuario hará uso de la tabla AspNetUsers que es generada en el contexto de UserIdentityContext
            _builder.Entity<Usuario>().ToTable("AspNetUsers");

            // Anidamos la tabla de Estado con los siguientes datos
            _builder.Entity<Estado>().HasData( new Estado { Id = 1, NombreEstado = "Completado" });
            _builder.Entity<Estado>().HasData( new Estado { Id = 2, NombreEstado = "Cancelado" });
            _builder.Entity<Estado>().HasData( new Estado { Id = 3, NombreEstado = "Retrasado" });
            _builder.Entity<Estado>().HasData( new Estado { Id = 4, NombreEstado = "En Proceso" });

            base.OnModelCreating(_builder);
        }
    }
}

