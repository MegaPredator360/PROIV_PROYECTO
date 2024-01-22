using Microsoft.AspNetCore.Identity;
using PROIV_PROYECTO.Contexts;
using PROIV_PROYECTO.Interface;
using PROIV_PROYECTO.Models;
using PROIV_PROYECTO.ModelsDTO;

namespace PROIV_PROYECTO.Services
{
    public class UsuarioService : IUsuarioService
    {
        public readonly UserManager<ApplicationUser> userManager;
        public readonly RoleManager<Permiso> roleManager;
        public readonly SignInManager<ApplicationUser> signInManager;
        public readonly ProyectoContext proyectoContext;

        public UsuarioService(UserManager<ApplicationUser> _userManager, RoleManager<Permiso> _roleManager, SignInManager<ApplicationUser> _signInManager, ProyectoContext _proyectoContext)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            signInManager = _signInManager;
            proyectoContext = _proyectoContext;
        }
        
        // Obtener todos los usuarios para index
        public IEnumerable<UsuarioListaDTO> ObtenerUsuario(string _filtrar, string _textoBusqueda)
        {
            if (_filtrar == "Nombre")
            {
                var result2 = proyectoContext.Users
                    .Join(proyectoContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
                    .Join(proyectoContext.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
                    .ToList()
                    .GroupBy(uv => new { uv.ur.u.Id, uv.ur.u.IdNumber, uv.ur.u.FullName, uv.ur.u.UserName, uv.ur.u.Email }).Select(r => new UsuarioListaDTO()
                    {
                        Id = r.Key.Id,
                        IdNumber = r.Key.IdNumber,
                        FullName = r.Key.FullName,
                        UserName = r.Key.UserName,
                        Email = r.Key.Email,
                        Role = string.Join(",", r.Select(c => c.r.Name).ToArray())
                    }).Where(u => u.FullName!.Contains(_textoBusqueda) || _textoBusqueda == null).ToList();
                
                return result2;
            }
            else if (_filtrar == "Cedula")
            {
                var result2 = proyectoContext.Users
                    .Join(proyectoContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
                    .Join(proyectoContext.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
                    .ToList()
                    .GroupBy(uv => new { uv.ur.u.Id, uv.ur.u.IdNumber, uv.ur.u.FullName, uv.ur.u.UserName, uv.ur.u.Email }).Select(r => new UsuarioListaDTO()
                    {
                        Id = r.Key.Id,
                        IdNumber = r.Key.IdNumber,
                        FullName = r.Key.FullName,
                        UserName = r.Key.UserName,
                        Email = r.Key.Email,
                        Role = string.Join(",", r.Select(c => c.r.Name).ToArray())
                    }).Where(u => u.IdNumber!.Contains(_textoBusqueda) || _textoBusqueda == null).ToList();
                
                return result2;
            }
            else if (_filtrar == "Permisos")
            {
                var result2 = proyectoContext.Users
                    .Join(proyectoContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
                    .Join(proyectoContext.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
                    .ToList()
                    .GroupBy(uv => new { uv.ur.u.Id, uv.ur.u.IdNumber, uv.ur.u.FullName, uv.ur.u.UserName, uv.ur.u.Email }).Select(r => new UsuarioListaDTO()
                    {
                        Id = r.Key.Id,
                        IdNumber = r.Key.IdNumber,
                        FullName = r.Key.FullName,
                        UserName = r.Key.UserName,
                        Email = r.Key.Email,
                        Role = string.Join(",", r.Select(c => c.r.Name).ToArray())
                    }).Where(u => u.Role!.Contains(_textoBusqueda) || _textoBusqueda == null).ToList();
                
                return result2;
            }
            else
            {
                var result2 = proyectoContext.Users
                    .Join(proyectoContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
                    .Join(proyectoContext.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
                    .ToList()
                    .GroupBy(uv => new { uv.ur.u.Id, uv.ur.u.IdNumber, uv.ur.u.FullName, uv.ur.u.UserName, uv.ur.u.Email }).Select(r => new UsuarioListaDTO()
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
        public async Task<StatusDTO> NuevoUsuarioAsync(UsuarioDTO _usuarioDTO)
        {
            var statusDTO = new StatusDTO();

            // Se verificara si el usuario nuevo existe en la base de datos
            var usuarioExiste = await userManager.FindByNameAsync(_usuarioDTO.UserName!);

            // Si el usuario existe
            if (usuarioExiste != null)
            {
                statusDTO.StatusCode = 0;
                statusDTO.Message = "El usuario ya existe";

                return statusDTO;
            }

            // Se convierten los datos a ApplicationUser
            ApplicationUser usuarioNuevo = new ApplicationUser()
            {
                Email = _usuarioDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = _usuarioDTO.UserName,
                FullName = _usuarioDTO.FullName,
                IdNumber = _usuarioDTO.IdNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            // Se crea el usuario, y se encripta la contraseña
            var resultado = await userManager.CreateAsync(usuarioNuevo, _usuarioDTO.Password!);
            
            // Si ocurrio un error durante la creacion del usuario
            if (!resultado.Succeeded)
            {
                statusDTO.StatusCode = 0;
                statusDTO.Message = "Ocurrio un error al crear el usuario";

                return statusDTO;
            }

            // Se busca el rol al cual se quiere ligar el usuario nuevo
            var roleName = proyectoContext.Roles.Where(r => r.Id == _usuarioDTO.Role).FirstOrDefault();

            // Se agregar el usuario y el rol en la tabla relacional AspNetUserRoles
            if (await roleManager.RoleExistsAsync(roleName!.Name!))
            {
                await userManager.AddToRoleAsync(usuarioNuevo, roleName.Name!);
            }

            statusDTO.StatusCode = 1;
            statusDTO.Message = null;

            return statusDTO;
        }

        // Obtener Id de Usuarios
        public UsuarioDTO ObtenerUsuarioId(string _usuarioId)
        {
            var result2 = proyectoContext.Users
                .Join(proyectoContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
                .Join(proyectoContext.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
                .ToList()
                .GroupBy(uv => new { uv.ur.u.Id, uv.ur.u.IdNumber, uv.ur.u.FullName, uv.ur.u.UserName, uv.ur.u.Email }).Select(r => new UsuarioDTO()
                {
                    Id = r.Key.Id,
                    IdNumber = r.Key.IdNumber,
                    FullName = r.Key.FullName,
                    UserName = r.Key.UserName,
                    Email = r.Key.Email,
                    Role = string.Join(",", r.Select(c => c.r.Id).ToArray())
                }).FirstOrDefault(p => p.Id == _usuarioId);

            return result2!;
        }

        public UsuarioDTO ObtenerUsuarioBorrarId(string _usuarioId)
        {
            var result2 = proyectoContext.Users
                .Join(proyectoContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
                .Join(proyectoContext.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
                .ToList()
                .GroupBy(uv => new { uv.ur.u.Id, uv.ur.u.IdNumber, uv.ur.u.FullName, uv.ur.u.UserName, uv.ur.u.Email }).Select(r => new UsuarioDTO()
                {
                    Id = r.Key.Id,
                    IdNumber = r.Key.IdNumber,
                    FullName = r.Key.FullName,
                    UserName = r.Key.UserName,
                    Email = r.Key.Email,
                    Role = string.Join(",", r.Select(c => c.r.Name).ToArray())
                }).FirstOrDefault(p => p.Id == _usuarioId);

            return result2!;
        }

        // Lista de Roles Registrados
        public UsuarioDropdownDTO UsuarioDropdownValues()
        {
            var response = new UsuarioDropdownDTO()
            {
                Permisos = proyectoContext.Roles.OrderBy(n => n.Name).ToList(),
            };

            return response;
        }

        // Actualizar Usuarios
        public async Task ActualizarUsuarioAsync(UsuarioDTO _usuarioDTO)
        {
            var usuarioEncontrado = await userManager.FindByIdAsync(_usuarioDTO.Id!);
            var permisoEncontrado = await userManager.GetRolesAsync(usuarioEncontrado!);

            if (await userManager.IsInRoleAsync(usuarioEncontrado!, permisoEncontrado.FirstOrDefault()!))
            {
                await userManager.RemoveFromRoleAsync(usuarioEncontrado!, permisoEncontrado.FirstOrDefault()!);
            }

            var permisoNombre = proyectoContext.Roles.Where(r => r.Id == _usuarioDTO.Role).FirstOrDefault();

            if (await roleManager.RoleExistsAsync(permisoNombre!.Name!))
            {
                await userManager.AddToRoleAsync(usuarioEncontrado!, permisoNombre.Name!);
            }

            usuarioEncontrado!.Email = _usuarioDTO.Email;
            usuarioEncontrado.NormalizedEmail = _usuarioDTO.Email!.ToUpper();
            usuarioEncontrado.UserName = _usuarioDTO.UserName;
            usuarioEncontrado.NormalizedUserName = _usuarioDTO.UserName!.ToUpper();
            usuarioEncontrado.FullName = _usuarioDTO.FullName;
            usuarioEncontrado.IdNumber = _usuarioDTO.IdNumber;
            
            proyectoContext.Update(usuarioEncontrado);
            proyectoContext.SaveChanges();
        }

        public async Task BorrarUsuario(UsuarioDTO _usuarioDTO)
        {
            var usuarioEncontrado = await userManager.FindByIdAsync(_usuarioDTO.Id!);
            var permisoEncontrado = await userManager.GetRolesAsync(usuarioEncontrado!);

            await userManager.RemoveFromRoleAsync(usuarioEncontrado!, permisoEncontrado.FirstOrDefault()!);
            
            proyectoContext.Remove(usuarioEncontrado);
            proyectoContext.SaveChanges();
        }
    }
}
