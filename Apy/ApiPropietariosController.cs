using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Inmobiliaria.Data;
using Inmobiliaria.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Microsoft.Extensions.Caching.Memory;

namespace Inmobiliaria.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ApiPropietariosController : ControllerBase
{
    private readonly DataContext contexto;
    private readonly IConfiguration configuration;
    private readonly IWebHostEnvironment environment;
    private readonly IMemoryCache cache;

    public ApiPropietariosController(
        DataContext contexto,
        IConfiguration config,
        IWebHostEnvironment env,
        IMemoryCache cache
    )
    {
        this.contexto = contexto;
        this.configuration = config;
        this.environment = env;
        this.cache = cache;
    }

    //------------------------------------------------------------- FUNCIONES AUXILIARES -------------------------------------------------------------//
    // Función auxiliar para generar una contraseña aleatoria
    private static string GenerarClaveAleatoria()
    {
        string chars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
        Random random = new Random();
        return new string(
            Enumerable.Repeat(chars, 24).Select(s => s[random.Next(s.Length)]).ToArray()
        );
    }

    // Función auxiliar para enviar correos electrónicos
    private async Task EnviarCorreo(string destinatario, string asunto, string cuerpo)
    {
        Console.WriteLine($"Enviando correo a: {destinatario} con el asunto: {asunto} y el cuerpo: {cuerpo}");
        var message = new MimeMessage();
        message.To.Add(new MailboxAddress("", destinatario));
        message.From.Add(new MailboxAddress("Inmobiliaria", configuration["SMTP:User"]));
        message.Subject = asunto;
        message.Body = new TextPart("html") { Text = cuerpo };

        using var client = new SmtpClient();
        client.Connect("smtp.gmail.com", 465, true);

        client.Authenticate(configuration["SMTP:User"], configuration["SMTP:Pass"]); // SMTP:User: edder709@gmail.com, SMTP:Pass: axqz sxby ilqy vtih
        await client.SendAsync(message);
        client.Disconnect(true);
    }

    //------------------------------------------------------------- Metodos o Verbos -------------------------------------------------------------//

    // POST: api/ApiPropietarios/ApiLogin
    [HttpPost("ApiLogin")]
    [AllowAnonymous]
    public IActionResult ApiLogin([FromForm] LoginView loginView)
    {
        try
        {
            // Buscar al propietario por correo
            var propietario = contexto.Propietario.FirstOrDefault(prop =>
                prop.Correo == loginView.Email
            );

            if (propietario == null)
            {
                return BadRequest("El correo no está registrado.");
            }

            // Hash de la contraseña ingresada
            string hashedPassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: loginView.Password,
                    salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8
                )
            );

            if (propietario.Contraseña != hashedPassword)
            {
                return BadRequest("Contraseña incorrecta.");
            }

            // Generación del token JWT
            var key = new SymmetricSecurityKey(
                System.Text.Encoding.ASCII.GetBytes(configuration["TokenAuthentication:SecretKey"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, propietario.Id_Propietario.ToString()),
            new Claim(ClaimTypes.Name, propietario.Correo),
            new Claim("FullName", propietario.Nombre + " " + propietario.Apellido),
            new Claim(ClaimTypes.Role, "Propietario")
        };

            var token = new JwtSecurityToken(
                issuer: configuration["TokenAuthentication:Issuer"],
                audience: configuration["TokenAuthentication:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    // GET: api/ApiPropietarios/MyPropietario
    [HttpGet("MyPropietario")]
    public async Task<IActionResult> MyPropietario()
    {
        try
        {
            // Obtén el correo del usuario logueado desde los claims del token JWT
            var userEmail = User.Identity?.Name;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("Usuario no logueado.");
            }

            // Buscar al propietario por correo
            var propietario = await contexto.Propietario.FirstOrDefaultAsync(p =>
                p.Correo == userEmail
            );

            if (propietario == null)
            {
                return NotFound("Propietario no encontrado.");
            }

            return Ok(propietario);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    // PUT: api/ApiPropietarios/UpdatePropietario
    [HttpPut("UpdatePropietario")]
    public async Task<IActionResult> UpdatePropietario([FromForm] Propietarios propietario)
    {
        try
        {
            // Obtener el correo del usuario logueado desde el token JWT
            var userEmail = User.Identity?.Name;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("Usuario no logueado.");
            }

            // Buscar al propietario actual por correo
            var propietarioExistente = await contexto.Propietario.FirstOrDefaultAsync(p =>
                p.Correo == userEmail
            );
            if (propietarioExistente == null)
            {
                return NotFound("Propietario no encontrado.");
            }

            // Actualizamos todos los campos que deben cambiar
            propietarioExistente.Nombre = propietario.Nombre;
            propietarioExistente.Apellido = propietario.Apellido;
            propietarioExistente.Dni = propietario.Dni;
            propietarioExistente.Direccion = propietario.Direccion;
            propietarioExistente.Telefono = propietario.Telefono;
            propietarioExistente.Correo = propietario.Correo;
            propietarioExistente.Avatar = propietario.Avatar;

            // Solo actualizar la contraseña si se proporcionó una nueva
            if (!string.IsNullOrEmpty(propietario.Contraseña))
            {
                propietarioExistente.Contraseña = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: propietario.Contraseña,
                        salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8
                    )
                );
            }

            // Guardamos los cambios
            contexto.Propietario.Update(propietarioExistente);
            await contexto.SaveChangesAsync();

            return Ok(propietarioExistente);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    // PUT: api/ApiPropietarios/ActualizarFoto
    [HttpPut("ActualizarFoto")]
    public async Task<IActionResult> ActualizarFoto([FromForm] IFormFile foto)
    {
        var userEmail = User.Identity?.Name;
        var propietario = await contexto.Propietario.FirstOrDefaultAsync(x => x.Correo == userEmail);
        if (propietario == null)
            return NotFound("Propietario no encontrado");

        if (foto == null || foto.Length == 0)
            return BadRequest("No se subió ninguna foto");

        // Guardar la imagen en la carpeta Uploads
        string wwwPath = environment.WebRootPath;
        string fileName = $"fotoperfil{propietario.Id_Propietario}{Path.GetExtension(foto.FileName)}";
        string path = Path.Combine(wwwPath, "/Uploads", fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await foto.CopyToAsync(stream);
        }

        propietario.Avatar = $"/Uploads/{fileName}";
        contexto.Propietario.Update(propietario);
        await contexto.SaveChangesAsync();

        return Ok(propietario);
    }

    
    [HttpPut("UpdatePassword")]
    public async Task<IActionResult> UpdatePassword([FromForm] ApiChangePass apiChangePass)
    {
        try
        {
            // Obtener el correo del usuario logueado desde el token JWT
            var userEmail = User.Identity?.Name;
            var propietario = await contexto.Propietario.FirstOrDefaultAsync(x => x.Correo == userEmail);
            if (propietario == null)
                return NotFound("Email no encontrado");

            // Hash de la vieja contraseña
            var oldpassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: apiChangePass.OldPassword,
                    salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8
                )
            );

            // Verificar si la clave anterior coincide
            if (!oldpassword.Equals(propietario.Contraseña))
                return BadRequest("La clave ingresada no coincide");

            // Hash de la nueva contraseña
            propietario.Contraseña = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: apiChangePass.NewPassword,
                    salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8
                )
            );

            contexto.Propietario.Update(propietario);
            await contexto.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok("Se ha actualizado la clave");
    }


    // POST: api/ApiPropietarios/RequestPasswordReset
    [HttpPost("RequestPasswordReset")]
    [AllowAnonymous]
    public async Task<IActionResult> RequestPasswordReset([FromForm] string email)
    {
        // Buscar el propietario por correo
        var propietario = await contexto.Propietario.FirstOrDefaultAsync(x => x.Correo == email);
        if (propietario == null)
        {
            return NotFound("Email no encontrado");
        }

        // Generar un token único (GUID)
        string token = Guid.NewGuid().ToString();

        // Guardar el token en caché con una expiración (ejemplo: 30 minutos)
        MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // El token expira en 30 minutos
        };
        cache.Set(token, propietario.Correo, cacheOptions); // Almacena el token con el correo asociado

        // Enviar correo de confirmación con el token en el enlace - Verificar la URL de la aplicación
        string resetUrl = $"{Request.Scheme}//https://ll3bj5xg-5058.brs.devtunnels.ms/api/ApiPropietarios/ConfirmResetPassword?token={token}";
        await EnviarCorreo(propietario.Correo, "Confirmación de Restablecimiento de Contraseña", $"Haga clic en el siguiente enlace para confirmar el restablecimiento de su contraseña: {resetUrl}");

        return Ok("Se ha enviado un correo de confirmación para restablecer la contraseña.");
    }

    // Confirmacion de restablecimiento de contraseña
    [HttpGet("ConfirmResetPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmResetPassword([FromQuery] string token)
    {
        // Verificar si el token existe en la caché
        if (!cache.TryGetValue(token, out string email))
        {
            return BadRequest("Token inválido o expirado.");
        }

        // Buscar el propietario por correo
        var propietario = await contexto.Propietario.FirstOrDefaultAsync(x => x.Correo == email);
        if (propietario == null)
        {
            return NotFound("Propietario no encontrado.");
        }

        // Generar una nueva clave aleatoria
        string nuevaClave = GenerarClaveAleatoria();
        Console.WriteLine($"La nueva clave es: {nuevaClave}");

        // Hashear la nueva contraseña
        propietario.Contraseña = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password: nuevaClave,
                salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8
            )
        );

        // Guardar los cambios en la base de datos
        await contexto.SaveChangesAsync();

        // Eliminar el token de la cache una vez usado
        cache.Remove(token);

        // Enviar un correo con la nueva contraseña sin hashear al propietario
        await EnviarCorreo(propietario.Correo, "Contraseña Restablecida", $"Su nueva clave es: {nuevaClave}");

        return Ok("Se ha restablecido la contraseña y se ha enviado un correo con la nueva clave.");
    }


}
