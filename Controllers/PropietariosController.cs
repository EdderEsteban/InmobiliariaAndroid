using System.Diagnostics;
using Inmobiliaria.Models;
using Inmobiliaria.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Utilities;

namespace Inmobiliaria.Controllers;

// Controlador para gestionar los propietarios
[Authorize]
public class PropietariosController : Controller
{
    // Logger para registrar eventos y errores
    private readonly ILogger<PropietariosController> _logger;

    // Repositorio para interactuar con la base de datos
    private readonly RepositorioPropietarios repositorio;

    // Constructor para inyectar dependencias
    public PropietariosController(ILogger<PropietariosController> logger)
    {
        _logger = logger;
        repositorio = new RepositorioPropietarios();
    }

    // Método para obtener la lista de propietarios
    public IActionResult ListadoPropietarios()
    {
        // Obtener la lista de propietarios desde el repositorio
        var lista = repositorio.ListarPropietarios();
        return View(lista);
    }

    // Método para crear un nuevo propietario
    public IActionResult CrearPropietario()
    {
        // Crear un nuevo propietario vacío
        return View(new Propietarios());
    }

    // Método para guardar un nuevo propietario
    [HttpPost]
    public IActionResult GuardarPropietario(Propietarios propietario)
    {
        // Verificar si el modelo es válido
        if (ModelState.IsValid)
        {
            // Asigna el Usuario que creo el registro
            var UserId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            propietario.Id_usuario = int.Parse(UserId);
            // Guardar el propietario en el repositorio
            repositorio.GuardarNuevo(propietario);
            // Redirección a la lista de propietarios
            return RedirectToAction(nameof(ListadoPropietarios));
        }
        // Si el modelo no es válido, devolver la vista con los errores
        return View("CrearPropietario", propietario);
    }

    // Método para editar un propietario existente
    public IActionResult EditarPropietario(int id)
    {
        // Obtener el propietario desde el repositorio
        var propietario = repositorio.ObtenerPropietario(id);
        return View(propietario);
    }

    // Método para actualizar un propietario existente
    [HttpPost]
    public IActionResult ModificarPropietario(Propietarios propietario)
    {
        // Verificar si el modelo es válido
        if (ModelState.IsValid)
        {
            // Actualizar el propietario en el repositorio
            repositorio.ActualizarPropietario(propietario);
            // Redirección a la lista de propietarios
            return RedirectToAction(nameof(ListadoPropietarios));
        }
        // Si el modelo no es válido, devolver la vista con los errores
        return View("EditarPropietario", propietario);
    }

    // Método para eliminar un propietario existente
    [Authorize(Policy = "Administrador")]
    public IActionResult EliminarPropietario(int id)
    {
        try
        {
            // Intentar eliminar el propietario desde el repositorio
            repositorio.EliminarPropietario(id);
            // Establecer mensaje de éxito
            TempData["SuccessMessage"] = "Propietario eliminado exitosamente.";
        }
        catch (InvalidOperationException ex)
        {
            // Capturar la excepción específica y establecer el mensaje de error
            TempData["ErrorMessage"] = ex.Message;
        }
        catch (Exception ex)
        {
            // Capturar cualquier otra excepción y establecer un mensaje de error genérico
            TempData["ErrorMessage"] = $"Ocurrió un error al eliminar el propietario. {ex.Message}";
        }

        // Redirigir a la lista de propietarios
        return RedirectToAction(nameof(ListadoPropietarios));
    }

    // Método para buscar un propietario
    public IActionResult BuscarPropietarios()
    {
        return View();
    }

    // Método para obtener inmuebles por propietario
    [HttpPost]
    public IActionResult ObtenerInmueblesPorPropietario([FromBody] int idPropietario)
    {
        try
        {
            RepositorioInmuebles repositorioInmuebles = new RepositorioInmuebles();
            var inmuebles = repositorioInmuebles.ListarInmueblesPorPropietario(idPropietario);
            return Json(inmuebles);
        }
        catch (Exception ex)
        {
            // Manejo de errores más detallado si es necesario
            return StatusCode(500, $"Error al obtener inmuebles: {ex.Message}");
        }
    }

    // Método para recibir el formulario Search
    [HttpPost]
    public IActionResult BuscarProp([FromBody] BusquedaPropietarios busqueda)
    {
        var resultados = repositorio.BuscarPropietarios(busqueda);
        // Devuelve los resultados como JSON
        return Json(resultados);
    }
}
