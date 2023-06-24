using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoProgra4v2.Models;
using System.Data;
using System.Reflection.Emit;
using System.Threading;

namespace ProyectoProgra4v2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
        IdentityUserClaim<string>,  IdentityUserRole<string>, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationRole>().HasData(new ApplicationRole { Name = "Administrador", NormalizedName = "ADMINISTRADOR", ConcurrencyStamp = null });
            builder.Entity<ApplicationRole>().HasData(new ApplicationRole { Name = "Gestor", NormalizedName = "GESTOR", ConcurrencyStamp = null });
            builder.Entity<ApplicationRole>().HasData(new ApplicationRole { Name = "Usuario", NormalizedName = "USUARIO", ConcurrencyStamp = null });
            base.OnModelCreating(builder);
        }
    }
}
