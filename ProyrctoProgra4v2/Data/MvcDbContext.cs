using Microsoft.EntityFrameworkCore;
using ProyectoProgra4v2.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ProyectoProgra4v2.Data
{
    public class MvcDbContext : DbContext
    {
        public MvcDbContext(DbContextOptions<MvcDbContext> options) : base(options)
        {

        }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<TareaUsuario> TareasUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<TareaLista> TareaListas { get; set; }
        public DbSet<TareaUsuarioLista> TareaUsuarioListas { get; set; }
        public DbSet<ProyectoLista> ProyectoListas { get; set; }
        public DbSet<ProyectoDetalle> ProyectoDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TareaUsuario>().HasKey(ut => new
            {
                ut.UsuarioId,
                ut.TareaId
            });

            modelBuilder.Entity<TareaUsuario>().HasOne(u => u.Usuario).WithMany(ut => ut.TareasUsuarios).HasForeignKey(u => u.UsuarioId);
            modelBuilder.Entity<TareaUsuario>().HasOne(t => t.Tarea).WithMany(ut => ut.TareasUsuarios).HasForeignKey(t => t.TareaId);

            modelBuilder.Ignore<TareaNew>();
            modelBuilder.Ignore<NewTareaDropdowns>();
            modelBuilder.Ignore<NewProyectoDropdowns>();
            modelBuilder.Ignore<ProyectoNew>();
            modelBuilder.Entity<Usuario>().ToTable("AspNetUsers");

            modelBuilder.Entity<Estado>().HasData( new Estado { Id = 1, NombreEstado = "Completado" });
            modelBuilder.Entity<Estado>().HasData( new Estado { Id = 2, NombreEstado = "Cancelado" });
            modelBuilder.Entity<Estado>().HasData( new Estado { Id = 3, NombreEstado = "Retrasado" });
            modelBuilder.Entity<Estado>().HasData( new Estado { Id = 4, NombreEstado = "En Proceso" });

            base.OnModelCreating(modelBuilder);
        }
    }
}

