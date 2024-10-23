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
public class ApiInmueblesController : ControllerBase
{
    private readonly DataContext contexto;
    private readonly IConfiguration configuration;
    private readonly IWebHostEnvironment environment;

    public ApiInmueblesController(
        DataContext contexto,
        IConfiguration config,
        IWebHostEnvironment env
    )
    {
        this.contexto = contexto;
        this.configuration = config;
        this.environment = env;
    }
    // GET: api/ApiInmuebles/MisInmuebles
    [HttpGet("MisInmuebles")]
    public async Task<IActionResult> MisInmuebles()
    {
        try
        {
            // Obtener el ID del propietario desde el token JWT (ClaimTypes.NameIdentifier)
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var propietarioId = int.Parse(idClaim);

            if (propietarioId == 0)
                return Unauthorized("Usuario no logueado.");



            // Obtener los inmuebles del propietario
            var inmuebles = await contexto.Inmueble
                .Include(t => t.Tipo)
                .Where(i => i.Id_propietario == propietarioId)
                .ToListAsync();

            return Ok(inmuebles);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    // GET: api/ApiInmuebles/BuscarInmueblePorId/{id}
    [HttpGet("BuscarInmueblePorId/{id}")]
    public async Task<IActionResult> BuscarInmueblePorId(int id)
    {
        try
        {
            // Buscar el inmueble por su ID, incluyendo su tipo y las fotos relacionadas
            var inmueble = await contexto.Inmueble
                .Include(i => i.Tipo) // Incluir el tipo de inmueble
                .Include(i => i.Fotos) // Incluir las fotos asociadas al inmueble
                .FirstOrDefaultAsync(i => i.Id_inmueble == id); // Filtrar por ID

            // Si no se encuentra el inmueble, devolver un error 404
            if (inmueble == null)
            {
                return NotFound($"No se encontró el inmueble con ID {id}.");
            }

            // Devolver el inmueble encontrado
            return Ok(inmueble);
        }
        catch (Exception ex)
        {
            // Manejar cualquier error inesperado
            return BadRequest($"Error: {ex.Message}");
        }
    }



    // POST: api/ApiInmuebles/SubirFotos
    [HttpPost("SubirFotos")]
    public async Task<IActionResult> SubirFotos(int inmuebleId, [FromForm] List<IFormFile> fotos)
    {
        Console.WriteLine("entrando a subir fotos");
        var inmueble = await contexto.Inmueble.FindAsync(inmuebleId);
        if (inmueble == null)
        {
            return NotFound("Inmueble no encontrado.");
        }

        string wwwPath = environment.WebRootPath;
        string path = Path.Combine(wwwPath, "Uploads", "InmueblesImg");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        foreach (var foto in fotos)
        {
            if (foto != null && foto.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(foto.FileName);
                var fullPath = Path.Combine(path, uniqueFileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await foto.CopyToAsync(stream);
                }

                var nuevaFoto = new FotosInmueble
                {
                    FotoUrl = Path.Combine("/Uploads/InmueblesImg", uniqueFileName),
                    Id_inmueble = inmuebleId
                };

                contexto.Fotos_inmueble.Add(nuevaFoto);
            }
        }

        await contexto.SaveChangesAsync();
        return Ok("Fotos subidas exitosamente.");
    }


}