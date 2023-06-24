using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoProgra4v2.Data.Services.Interface;
using ProyectoProgra4v2.Models;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoProgra4v2.Data.Services
{
    public class UsuarioService : IUsuarioService
    {
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly RoleManager<ApplicationRole> _roleManager;
        public readonly SignInManager<ApplicationUser> _signInManager;
        public readonly ApplicationDbContext _context;

        public UsuarioService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }
        // Obtener todos los usuarios para index
        public IEnumerable<UsuarioLista> GetUsuarios(string SearchBy, string SearchString)
        {
            if (SearchBy == "Nombre")
            {
                var result2 = _context.Users
               .Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
               .Join(_context.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
               .ToList()
               .GroupBy(uv => new { uv.ur.u.Id, uv.ur.u.IdNumber, uv.ur.u.FullName, uv.ur.u.UserName, uv.ur.u.Email }).Select(r => new UsuarioLista()
               {
                   Id = r.Key.Id,
                   IdNumber = r.Key.IdNumber,
                   FullName = r.Key.FullName,
                   UserName = r.Key.UserName,
                   Email = r.Key.Email,
                   Role = string.Join(",", r.Select(c => c.r.Name).ToArray())
               }).Where(u => u.FullName.Contains(SearchString) || SearchString == null).ToList();
                return result2;
            }
            else if (SearchBy == "Cedula")
            {
                var result2 = _context.Users
               .Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
               .Join(_context.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
               .ToList()
               .GroupBy(uv => new { uv.ur.u.Id, uv.ur.u.IdNumber, uv.ur.u.FullName, uv.ur.u.UserName, uv.ur.u.Email }).Select(r => new UsuarioLista()
               {
                   Id = r.Key.Id,
                   IdNumber = r.Key.IdNumber,
                   FullName = r.Key.FullName,
                   UserName = r.Key.UserName,
                   Email = r.Key.Email,
                   Role = string.Join(",", r.Select(c => c.r.Name).ToArray())
               }).Where(u => u.IdNumber.Contains(SearchString) || SearchString == null).ToList();
                return result2;
            }
            else if (SearchBy == "Permisos")
            {
                var result2 = _context.Users
               .Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
               .Join(_context.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
               .ToList()
               .GroupBy(uv => new { uv.ur.u.Id, uv.ur.u.IdNumber, uv.ur.u.FullName, uv.ur.u.UserName, uv.ur.u.Email }).Select(r => new UsuarioLista()
               {
                   Id = r.Key.Id,
                   IdNumber = r.Key.IdNumber,
                   FullName = r.Key.FullName,
                   UserName = r.Key.UserName,
                   Email = r.Key.Email,
                   Role = string.Join(",", r.Select(c => c.r.Name).ToArray())
               }).Where(u => u.Role.Contains(SearchString) || SearchString == null).ToList();
                return result2;
            }
            else
            {
                var result2 = _context.Users
               .Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
               .Join(_context.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
               .ToList()
               .GroupBy(uv => new { uv.ur.u.Id, uv.ur.u.IdNumber, uv.ur.u.FullName, uv.ur.u.UserName, uv.ur.u.Email }).Select(r => new UsuarioLista()
               {
                   Id = r.Key.Id,
                   IdNumber = r.Key.IdNumber,
                   FullName = r.Key.FullName,
                   UserName = r.Key.UserName,
                   Email = r.Key.Email,
                   Role = string.Join(",", r.Select(c => c.r.Name).ToArray())
               }).ToList();
                return result2;
            }
        }

        // Añadir Usuarios
        public async Task<Status> AddUsuarioAsync(UsuarioDatos usuarioDatos)
        {
            var status = new Status();
            var userExists = await _userManager.FindByNameAsync(usuarioDatos.UserName);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "El usuario ya existe";
                return status;
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = usuarioDatos.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = usuarioDatos.UserName,
                FullName = usuarioDatos.FullName,
                IdNumber = usuarioDatos.IdNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var result = await _userManager.CreateAsync(user, usuarioDatos.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "Ocurrio un error al crear el usuario";
                return status;
            }

            var roleName = _context.Roles.Where(r => r.Id == usuarioDatos.Role).FirstOrDefault();

            if (await _roleManager.RoleExistsAsync(roleName.Name))
            {
                await _userManager.AddToRoleAsync(user, roleName.Name);
            }

            status.StatusCode = 1;
            status.Message = null;
            return status;
        }

        // Obtener Id de Usuarios
        public UsuarioActualizar GetUsuarioId(string Id)
        {
            var result2 = _context.Users
               .Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
               .Join(_context.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
               .ToList()
               .GroupBy(uv => new { uv.ur.u.Id, uv.ur.u.IdNumber, uv.ur.u.FullName, uv.ur.u.UserName, uv.ur.u.Email }).Select(r => new UsuarioActualizar()
               {
                   Id = r.Key.Id,
                   IdNumber = r.Key.IdNumber,
                   FullName = r.Key.FullName,
                   UserName = r.Key.UserName,
                   Email = r.Key.Email,
                   Role = string.Join(",", r.Select(c => c.r.Id).ToArray())
               }).FirstOrDefault(p => p.Id == Id);
              return result2;
        }

        public UsuarioActualizar GetUsuaDeleId(string Id)
        {
            var result2 = _context.Users
               .Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
               .Join(_context.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
               .ToList()
               .GroupBy(uv => new { uv.ur.u.Id, uv.ur.u.IdNumber, uv.ur.u.FullName, uv.ur.u.UserName, uv.ur.u.Email }).Select(r => new UsuarioActualizar()
               {
                   Id = r.Key.Id,
                   IdNumber = r.Key.IdNumber,
                   FullName = r.Key.FullName,
                   UserName = r.Key.UserName,
                   Email = r.Key.Email,
                   Role = string.Join(",", r.Select(c => c.r.Name).ToArray())
               }).FirstOrDefault(p => p.Id == Id);
            return result2;
        }

        // Lista de Roles Registrados
        public NewUsuarioDropdowns GetNewUsuarioDropdownsValues()
        {
            var response = new NewUsuarioDropdowns()
            {
                appRoles = _context.Roles.OrderBy(n => n.Name).ToList(),
            };

            return response;
        }

        // Actualizar Usuarios
        public async Task<ApplicationUser> UpdateUsuario(UsuarioActualizar usuarioActualizar)
        {
            var user = await _userManager.FindByIdAsync(usuarioActualizar.Id);
            var role = await _userManager.GetRolesAsync(user);

            if (await _userManager.IsInRoleAsync(user, role.FirstOrDefault()))
            {
                await _userManager.RemoveFromRoleAsync(user, role.FirstOrDefault());
            }

            var roleName = _context.Roles.Where(r => r.Id == usuarioActualizar.Role).FirstOrDefault();

            if (await _roleManager.RoleExistsAsync(roleName.Name))
            {
                await _userManager.AddToRoleAsync(user, roleName.Name);
            }

            user.Email = usuarioActualizar.Email;
            user.NormalizedEmail = usuarioActualizar.Email.ToUpper();
            user.UserName = usuarioActualizar.UserName;
            user.NormalizedUserName = usuarioActualizar.UserName.ToUpper();
            user.FullName = usuarioActualizar.FullName;
            user.IdNumber = usuarioActualizar.IdNumber;
            
            _context.Update(user);
            _context.SaveChanges();
            return user;
        }

        public async Task<ApplicationUser> DeleteUsuario(UsuarioActualizar usuarioActualizar)
        {
            var user = await _userManager.FindByIdAsync(usuarioActualizar.Id);
            var role = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRoleAsync(user, role.FirstOrDefault());
            
            _context.Remove(user);
            _context.SaveChanges();
            return user;
        }
    }
}
