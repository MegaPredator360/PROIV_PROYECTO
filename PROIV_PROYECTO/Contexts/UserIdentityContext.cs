using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PROIV_PROYECTO.Models;
using System.Data;
using System.Reflection.Emit;
using System.Threading;

namespace PROIV_PROYECTO.Contexts
{
    public class UserIdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
        IdentityUserClaim<string>,  IdentityUserRole<string>, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public UserIdentityContext(DbContextOptions<UserIdentityContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder _builder)
        {
            // Se definirán los ID del Rol de Administrador, y el ID de Administrador
            const string Admin_Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string Role_id = "ad376a8f-9eab-4bb9-9fca-30b01540f445";

            // Se crea una variable que se encargará de encriptar la contraseña
            var hasher = new PasswordHasher<Application_user>(); 

            // Se crean los roles que se utilizarán
            _builder.Entity<ApplicationRole>().HasData(new ApplicationRole { Id = Role_id, Name = "Administrador", NormalizedName = "ADMINISTRADOR", ConcurrencyStamp = null });
            _builder.Entity<ApplicationRole>().HasData(new ApplicationRole { Name = "Gestor", NormalizedName = "GESTOR", ConcurrencyStamp = null });
            _builder.Entity<ApplicationRole>().HasData(new ApplicationRole { Name = "Usuario", NormalizedName = "USUARIO", ConcurrencyStamp = null });

            // Se crea el usuario administrador
            _builder.Entity<Application_user>().HasData(new Application_user{Id = Admin_Id, IdNumber = "1", FullName = "Usuario Administrador", UserName = "admin", NormalizedUserName = "ADMIN", Email = "admin@correo.com", NormalizedEmail = "ADMIN@CORREO.COM", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "12345"), SecurityStamp = string.Empty}); 

            // Se ligará este usuario administrador con el rol
            _builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>{RoleId = Role_id, UserId = Admin_Id});

            
            base.OnModelCreating(_builder);
        }
    }
}
