using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Models;

namespace PROIV_PROYECTO.Contexts
{
    public class UsuarioContext : IdentityDbContext<ApplicationUser, Permiso, string,
        IdentityUserClaim<string>,  IdentityUserRole<string>, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder _builder)
        {
            // Se definirán los ID del Rol de Administrador, y el ID de Administrador
            const string usuarioAdminId = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string permisoAdminId = "ad376a8f-9eab-4bb9-9fca-30b01540f445";

            // Se crea una variable que se encargará de encriptar la contraseña
            var hasher = new PasswordHasher<ApplicationUser>(); 

            // Se crean los roles que se utilizarán
            _builder.Entity<Permiso>().HasData(new Permiso { Id = permisoAdminId, Name = "Administrador", NormalizedName = "ADMINISTRADOR", ConcurrencyStamp = null });
            _builder.Entity<Permiso>().HasData(new Permiso { Name = "Gestor", NormalizedName = "GESTOR", ConcurrencyStamp = null });
            _builder.Entity<Permiso>().HasData(new Permiso { Name = "Usuario", NormalizedName = "USUARIO", ConcurrencyStamp = null });

            // Se crea el usuario administrador
            _builder.Entity<ApplicationUser>().HasData(new ApplicationUser{Id = usuarioAdminId, IdNumber = "1", FullName = "Usuario Administrador", UserName = "admin", NormalizedUserName = "ADMIN", Email = "admin@correo.com", NormalizedEmail = "ADMIN@CORREO.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "12345"), SecurityStamp = string.Empty}); 

            // Se ligará este usuario administrador con el rol
            _builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>{RoleId = permisoAdminId, UserId = usuarioAdminId});

            
            base.OnModelCreating(_builder);
        }
    }
}
