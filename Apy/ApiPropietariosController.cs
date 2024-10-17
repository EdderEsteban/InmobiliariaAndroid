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

namespace Inmobiliaria.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ApiPropietariosController : ControllerBase
{
    private readonly DataContext contexto;
    private readonly IConfiguration configuration;
    private readonly IWebHostEnvironment environment;

    public ApiPropietariosController(
        DataContext contexto,
        IConfiguration config,
        IWebHostEnvironment env
    )
    {
        this.contexto = contexto;
        this.configuration = config;
        this.environment = env;
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

    // PUT: api/ApiPropietarios/ResetPassword
    [HttpPut("ResetPassword")]
    [AllowAnonymous]// borrar luego de las pruebas
    public async Task<IActionResult> ResetPassword()
    {
        try
        {
            // Obtener el correo del usuario logueado desde el token JWT
            var userEmail = User.Identity?.Name;
            var propietario = await contexto.Propietario.FirstOrDefaultAsync(x => x.Correo == userEmail);

            if (propietario == null)
                return NotFound("Email no encontrado");

            string nuevaClave = GenerarClaveAleatoria();
            Console.WriteLine($"La nueva clave es: {nuevaClave}");
            // Hash de la nueva contraseña
            propietario.Contraseña = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: nuevaClave,
                    salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8
                )
            );
            Console.WriteLine($"El Propietario {propietario.Nombre} {propietario.Apellido} ha solicitado restablecer su contraseña y es {propietario.Contraseña} enviada al correo: {propietario.Correo}");
            await contexto.SaveChangesAsync();

            // Enviar correo con la nueva clave
            await EnviarCorreo(propietario.Correo, "Restablecimiento de contraseña", $"Su nueva clave es: {nuevaClave}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok("Se ha enviado un correo con la nueva clave");
    }

    [HttpPut("UpdatePassword")]
    public async Task<IActionResult> UpdatePassword([FromForm] string oldpass, [FromForm] string newpassword)
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
                    password: oldpass,
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
                    password: newpassword,
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

}
