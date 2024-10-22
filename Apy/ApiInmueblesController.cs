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

    // Funcion Auxiliar para Enum Uso
    private static UsoInmueble ParseUso(string uso)
    {
        if (Enum.TryParse(uso, out UsoInmueble usoEnum))
        {
            return usoEnum;
        }
        return default(UsoInmueble);
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

    
}