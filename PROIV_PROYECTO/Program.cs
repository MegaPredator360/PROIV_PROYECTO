using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROIV_PROYECTO.Contexts;
using PROIV_PROYECTO.Services;
using PROIV_PROYECTO.Interface;
using PROIV_PROYECTO.Models;

var builder = WebApplication.CreateBuilder(args);

// Se identifican los contextos a utilizar para la base de datos
builder.Services.AddDbContext<ProyectoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Validar Identidad de Usuario
builder.Services.AddIdentity<ApplicationUser, Permiso>()
    .AddEntityFrameworkStores<ProyectoContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();

// Pagina para Inicio de Sesion
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Authentication/Login";
    options.AccessDeniedPath = "/Authentication/AccessDenied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
