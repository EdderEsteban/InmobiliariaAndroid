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
        var message = new MimeMessage();
        message.To.Add(new MailboxAddress("", destinatario));
        message.From.Add(new MailboxAddress("Inmobiliaria", configuration["SMTPUser"]));
        message.Subject = asunto;
        message.Body = new TextPart("html") { Text = cuerpo };

        using var client = new SmtpClient();
        client.Connect("smtp.gmail.com", 465, true);
        client.Authenticate(configuration["SMTPUser"], configuration["SMTPPass"]);
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

    // GET: api/ApiPropietarios/AllPropietarios
    [HttpGet("AllPropietarios")] // Metodo al vicio
    public async Task<IActionResult> AllPropietarios()
    {
        Console.WriteLine("entrando a llamar a todos los propietarios");
        try
        {
            var propietarios = await contexto.Propietario.ToListAsync();
            return Ok(propietarios);
        }
        catch (Exception ex)
        {
            // Agregar más detalles sobre el error
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            return BadRequest($"Exception: {ex.Message}");
        }
    }

    // POST: api/ApiPropietarios/NewPropietario
    [HttpPost("NewPropietario")] // al vicio
    [AllowAnonymous]
    public async Task<IActionResult> NewPropietario([FromForm] Propietarios propietario)
    {
        try
        {
            Console.WriteLine("entrando a crear un nuevo propietario");
            // Hash de la contraseña antes de guardar
            propietario.Contraseña = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: propietario.Contraseña,
                    salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8
                )
            );

            contexto.Propietario.Add(propietario);
            await contexto.SaveChangesAsync();
            return CreatedAtAction(
                nameof(NewPropietario),
                new { id = propietario.Id_Propietario },
                propietario
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: api/ApiPropietarios/SearchPropietario/{id}
    [HttpGet("SearchPropietario/{id}")]
    public async Task<IActionResult> SearchPropietario(int id)
    {
        try
        {
            var propietario = await contexto.Propietario.FindAsync(id);
            if (propietario == null)
                return NotFound();

            return Ok(propietario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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

    // PUT: api/ApiPropietarios/UpdatePropietario/{id}
    [HttpPut("UpdatePropietario/{id}")] //aca voy controlando
    public async Task<IActionResult> UpdatePropietario(int id, [FromForm] Propietarios propietario)
    {
        var propietarioExistente = await contexto.Propietario.FindAsync(id);
        if (propietarioExistente == null)
        {
            return NotFound("Propietario no encontrado");
        }

        // Actualizamos solo los campos que deben cambiar
        propietarioExistente.Nombre = propietario.Nombre;
        propietarioExistente.Apellido = propietario.Apellido;
        propietarioExistente.Telefono = propietario.Telefono;
        propietarioExistente.Correo = propietario.Correo;

        // Solo actualizar la contraseña si fue proporcionada
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

        contexto.Propietario.Update(propietarioExistente);
        await contexto.SaveChangesAsync();

        return Ok(propietarioExistente);
    }

    // PUT: api/ApiPropietarios/UpdatePropietario
    [HttpPut("UpdatePropietario")]
    [Authorize] // Requiere que el usuario esté autenticado
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

    // DELETE: api/ApiPropietarios/DellPropietario/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DellPropietario(int id)
    {
        var propietario = await contexto.Propietario.FindAsync(id);
        if (propietario == null)
        {
            return NotFound("Propietario no encontrado");
        }

        contexto.Propietario.Remove(propietario);
        await contexto.SaveChangesAsync();

        return Ok($"Propietario con id {id} eliminado correctamente");
    }

    // PUT: api/ApiPropietarios/ActualizarFoto/{id}
    [HttpPut("ActualizarFoto/{id}")]
    public async Task<IActionResult> ActualizarFoto(int id, [FromForm] IFormFile foto)
    {
        var propietario = await contexto.Propietario.FindAsync(id);
        if (propietario == null)
            return NotFound("Propietario no encontrado");

        if (foto == null || foto.Length == 0)
            return BadRequest("No se subió ninguna foto");

        string wwwPath = environment.WebRootPath;
        string fileName = $"fotoperfil{id}{Path.GetExtension(foto.FileName)}";
        string path = Path.Combine(wwwPath, "fotos", fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await foto.CopyToAsync(stream);
        }

        propietario.Avatar = $"/Uploads/{fileName}";
        contexto.Propietario.Update(propietario);
        await contexto.SaveChangesAsync();

        return Ok(propietario);
    }

    // POST: api/ApiPropietarios/ResetPassword
    [HttpPost("ResetPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromForm] string email)
    {
        var propietario = await contexto.Propietario.FirstOrDefaultAsync(x => x.Correo == email);
        if (propietario == null)
            return NotFound("Email no encontrado");

        string nuevaClave = GenerarClaveAleatoria();

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

        await contexto.SaveChangesAsync();

        // Enviar correo con la nueva clave
        await EnviarCorreo(
            propietario.Correo,
            "Restablecimiento de contraseña",
            $"Su nueva clave es: {nuevaClave}"
        );

        return Ok("Se ha enviado un correo con la nueva clave");
    }
}
