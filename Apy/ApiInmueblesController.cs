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
            var inmuebles = await contexto
                .Inmueble.Include(t => t.Tipo)
                .Include(i => i.Fotos) // Incluir las fotos asociadas al inmueble
                .Where(i => i.Id_Propietario == propietarioId)
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
            // Buscar el inmueble por su ID
            var inmueble = await contexto
                .Inmueble.Include(i => i.Tipo) // Incluir el tipo de inmueble
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

    // POST: api/ApiInmuebles/AltaInmueble
    [HttpPost("AltaInmueble")]
    public async Task<IActionResult> AltaInmueble([FromBody] ApiInmuebles inmueble)
    {
        Console.WriteLine("entrando a alta inmueble");
        try
        {
            // Validar los datos del inmueble
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Obtener el ID del propietario desde el token JWT (ClaimTypes.NameIdentifier)
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var propietarioId = int.Parse(idClaim);

            if (propietarioId == 0)
                return Unauthorized("Usuario no logueado.");

            // Asignar el ID del propietario al inmueble
            inmueble.Id_Propietario = propietarioId;

            // Guardar el inmueble en la base de datos
            contexto.Inmueble.Add(inmueble);
            await contexto.SaveChangesAsync();

            return Ok(inmueble);
        }
        catch (Exception ex)
        {
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
                var uniqueFileName =
                    Guid.NewGuid().ToString() + "_" + Path.GetFileName(foto.FileName);
                var fullPath = Path.Combine(path, uniqueFileName);

                // Guardar el archivo en el servidor
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await foto.CopyToAsync(stream);
                }

                // Guardar la URL relativa con el formato correcto (reemplazar \ por /)
                var relativeUrl = $"/Uploads/InmueblesImg/{uniqueFileName}".Replace("\\", "/");

                var nuevaFoto = new FotosInmueble
                {
                    FotoUrl = relativeUrl,
                    Id_inmueble = inmuebleId
                };

                contexto.Fotos_inmueble.Add(nuevaFoto);
            }
        }

        await contexto.SaveChangesAsync();
        return Ok("Fotos subidas exitosamente.");
    }

    // PATCH: api/ApiInmuebles/ActualizarEstado/{id}
    [HttpPatch("ActualizarEstado/{id}")]
    public async Task<IActionResult> ActualizarEstadoActivo(int id, [FromForm] bool activo)
    {
        try
        {
            // Obtener el inmueble por su ID
            var inmueble = await contexto.Inmueble.FindAsync(id);

            // Verificar si el inmueble existe
            if (inmueble == null)
            {
                return NotFound($"No se encontró el inmueble con ID {id}.");
            }

            // Actualizar el estado de 'Activo' con el valor recibido en el cuerpo de la solicitud
            inmueble.Activo = activo;

            // Guardar cambios en la base de datos
            contexto.Inmueble.Update(inmueble);
            await contexto.SaveChangesAsync();

            // Retornar la respuesta con el nuevo estado de 'Activo'
            return Ok(new { id = inmueble.Id_inmueble, activo = inmueble.Activo });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    // GET: api/ApiInmuebles/ListadodeTipos
    [HttpGet("ListadodeTipos")]
    public async Task<IActionResult> ListadodeTipos()
    {
        try
        {
            var tipos = await contexto.Tipo_inmueble.ToListAsync();
            return Ok(tipos);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    // GET: api/ApiInmuebles/ListadodeContratos/{id}
    [HttpGet("ListadodeContratos/{id}")]
    public async Task<IActionResult> ListadodeContratos(int id)
    {
        Console.WriteLine("Entrando a ListadodeContratos");
        try
        {
            // Obtener todos los contratos para el inmueble con el ID especificado
            var contratos = await contexto.Contrato
            .Where(c => c.Id_inmueble == id)
            .ToListAsync();

            // Verificar si existen contratos asociados al inmueble
            if (contratos == null || !contratos.Any())
                return NotFound("No se encontraron contratos para el inmueble especificado.");
                foreach (var contrato in contratos){
                    Console.WriteLine($"Contratos encontrados: {contratos[0].Id_contrato}");
                }
                ;
            return Ok(contratos);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    // GET: api/ApiInmuebles/ListadodePagos/{id}
    [HttpGet("ListadodePagos/{id}")]
    public async Task<IActionResult> ListadodePagos(int id)
    {
        try
        {
            // Obtener todos los pagos asociados al contrato con el ID especificado
            var pagos = await contexto.Pago
            .Where(p => p.Id_Contrato == id)
            .ToListAsync();

            // Verificar si existen pagos asociados al contrato
            if (pagos == null || !pagos.Any())
                return NotFound("No se encontraron pagos para el contrato especificado.");

            return Ok(pagos);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}
