using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Models;
using PROIV_PROYECTO.ModelsDTO;

namespace PROIV_PROYECTO.Contexts
{
    public class ProyectoContext : IdentityDbContext<Usuario, Permiso, string,
        IdentityUserClaim<string>,  IdentityUserRole<string>, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ProyectoContext(DbContextOptions<ProyectoContext> options) : base(options) { }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<TareaUsuario> TareasUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Estado> Estados { get; set; }

        protected override void OnModelCreating(ModelBuilder _builder)
        {
            // Se definirán los ID del Rol de Administrador, y el ID de Administrador
            const string usuarioAdminId = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string permisoAdminId = "ad376a8f-9eab-4bb9-9fca-30b01540f445";

            // Se crea una variable que se encargará de encriptar la contraseña
            var hasher = new PasswordHasher<Usuario>(); 

            // Se crean los roles que se utilizarán
            _builder.Entity<Permiso>().HasData(new Permiso { Id = permisoAdminId, Name = "Administrador", NormalizedName = "ADMINISTRADOR", ConcurrencyStamp = null });
            _builder.Entity<Permiso>().HasData(new Permiso { Name = "Gestor", NormalizedName = "GESTOR", ConcurrencyStamp = null });
            _builder.Entity<Permiso>().HasData(new Permiso { Name = "Usuario", NormalizedName = "USUARIO", ConcurrencyStamp = null });

            // Se crea el usuario administrador
            _builder.Entity<Usuario>().HasData(new Usuario{Id = usuarioAdminId, IdNumber = "1", FullName = "Usuario Administrador", UserName = "admin", NormalizedUserName = "ADMIN", Email = "admin@correo.com", NormalizedEmail = "ADMIN@CORREO.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "12345"), SecurityStamp = string.Empty}); 

            // Se ligará este usuario administrador con el rol
            _builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>{RoleId = permisoAdminId, UserId = usuarioAdminId});

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
