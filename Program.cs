using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Inmobiliaria.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var configuration = builder.Configuration;

/* PARA MySql - usando Pomelo */
builder.Services.AddDbContext<DataContext>(
    options => options.UseMySql(
        configuration["ConnectionStrings:DefaultConnection"],
        ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"])
        )
    );

// Servicio de Autenticación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>//el sitio web valida con cookie
	{
		options.LoginPath = "/Usuarios/Login";
		options.LogoutPath = "/Usuarios/Logout";
		options.AccessDeniedPath = "/Home/Restringido";
		options.ExpireTimeSpan = TimeSpan.FromMinutes(5);//Tiempo de expiración
	});

    // Políticas de Autorización
builder.Services.AddAuthorization(options =>
{
    // Política para Administradores
    options.AddPolicy("Administrador", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Administrador"));

    // Política para Empleados
    options.AddPolicy("Empleado", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Empleado"));

    // Política combinada para Administradores y Empleados
    options.AddPolicy("Propietario", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Propietario"));
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

// Habilitar CORS para recibir todas las solicitudes
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// app para usar archivos estáticos (cargar las imagenes)
app.UseStaticFiles();

app.UseRouting();

// Usar autenticacion y autorizacion
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
