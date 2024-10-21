using System.Diagnostics;
using Inmobiliaria.Models;
using Inmobiliaria.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Controllers;

// Controlador para gestionar los propietarios
[Authorize]
public class InmueblesController : Controller
{
    // Logger para registrar eventos y errores
    private readonly ILogger<InmueblesController> _logger;

    // Repositorio para interactuar con la base de datos
    private readonly RepositorioInmuebles repositorio;

    // Constructor para inyectar dependencias
    public InmueblesController(ILogger<InmueblesController> logger)
    {
        _logger = logger;
        repositorio = new RepositorioInmuebles();
    }

    // Método para obtener la lista de propietarios
    [HttpGet]
    public IActionResult ListadoTodosInmuebles()
    {
        // Obtener la lista de propietarios desde el repositorio
        var lista = repositorio.ListarTodosInmuebles();
        return View(lista);
    }

    // Metodo para listar todos los Inmuebles alquilados
    [HttpGet]
    public IActionResult ListadoInmueblesAlquilados()
    {
        var lista = repositorio.ListarInmueblesAlquilados();
        return Json(new { success = true, data = lista });
    }

    // Metodo para listar todos los Inmuebles Disponibles
    [HttpGet]
    public IActionResult ListadoInmueblesDisponibles()
    {
        var lista = repositorio.ListarInmueblesDisponibles();
        return Json(new { success = true, data = lista });
    }

    // Metodo para listar todos los Inmuebles Inactivos
    [HttpGet]
    public IActionResult ListadoInmueblesInactivos()
    {
        var lista = repositorio.ListarInmueblesInactivos();
        return Json(new { success = true, data = lista });
    }

    // Metodo para editar un inmueble
    [HttpGet]
    public IActionResult EditarInmueble(int id, string viewName)
    {
        // Guardar el nombre de la vista original en TempData
        TempData["viewName"] = viewName;

        //Enviar la lista de tipos de inmueble
        var listTipos = repositorio.ListarTiposInmueble();
        ViewBag.tipos = listTipos;

        //Enviar la lista de propietarios
        RepositorioPropietarios repoProp = new RepositorioPropietarios();
        var listPropietarios = repoProp.ListarPropietarios();
        ViewBag.propietarios = listPropietarios;

        var inmueble = repositorio.ObtenerInmueble(id);

        return View(inmueble);
    }

    // Metodo para modificar un inmueble
    [HttpPost]
    public IActionResult ModificarInmueble(Inmuebles inmueble)
    {
        if (ModelState.IsValid)
        {
            // Crear el repositorio de Contratos
            RepositorioContratos repoContrato = new RepositorioContratos();

            // Obtener todos los contratos asociados al inmueble
            var contratosDelInmueble = repoContrato.ListarContratosPorInmueble(
                inmueble.Id_inmueble
            );

            // Verificar si existen contratos asociados al inmueble
            if (contratosDelInmueble.Count > 0)
            {
                // Si hay contratos, ver si Inmueble.Disponible es 1 o 0
                Inmuebles? inmuebleOriginal = repositorio.ObtenerInmueble(inmueble.Id_inmueble); // Obtener el inmueble original de la BD
                if (inmuebleOriginal != null)
                {
                    if (inmuebleOriginal.Disponible == true)
                    {
                        // Permitir la modificación de todos los campos
                        repositorio.ActualizarInmueble(inmueble);
                    }
                    else
                    {
                        // Permitir la modificación de todos los campos excepto Disponible
                        inmueble.Disponible = inmuebleOriginal.Disponible; // Mantener el valor original de Disponible
                        repositorio.ActualizarInmuebleExceptoDisponible(inmueble);
                        TempData["AlertMessage"] =
                            "Se actualizó los datos excepto Disponible, ya que el inmueble tiene un contrato activo.";
                    }
                }
                else
                {
                    // No se encontro el inmueble original en la BD
                    return View("EditarInmueble", inmueble);
                }
            }
            else
            {
                // No hay contratos asociados, permitir la modificación de todos los campos
                repositorio.ActualizarInmueble(inmueble);
            }
            // Obtener el nombre de la vista original desde TempData
            string viewName = TempData["viewName"]?.ToString() ?? string.Empty;
            if (!string.IsNullOrEmpty(viewName))
            {
                return RedirectToAction(viewName);
            }
            return RedirectToAction(nameof(ListadoTodosInmuebles));
        }

        return View("EditarInmueble", inmueble);
    }

    //Metodo para crear un inmueble
    [HttpGet]
    public IActionResult CrearInmueble()
    {
        //Enviar la lista de tipos de inmueble
        var listTipos = repositorio.ListarTiposInmueble();
        ViewBag.tipos = listTipos;

        //Enviar la lista de propietarios
        RepositorioPropietarios repoProp = new RepositorioPropietarios();
        var listPropietarios = repoProp.ListarPropietarios();
        ViewBag.propietarios = listPropietarios;

        return View();
    }

    //Metodo para guardar un inmueble
    [HttpPost]
    public IActionResult GuardarInmueble(Inmuebles inmueble)
    {
        if (ModelState.IsValid) //Asegurarse q es valido el modelo
        {
            repositorio.GuardarNuevo(inmueble);
            return RedirectToAction(nameof(ListadoTodosInmuebles));
        }
        return View();
    }

    // Método para eliminar un inmueble
    [Authorize(Policy = "Administrador")]
    public IActionResult EliminarInmueble(int id)
    {
        var contrato = new RepositorioContratos().InmuebleTieneContrato(id);
        if (contrato > 0)
        {
            TempData["ErrorMessage"] =
                "No se puede eliminar el inmueble porque tiene un contrato activo.";
            return RedirectToAction(nameof(ListadoTodosInmuebles));
        }
        else
        {
            repositorio.EliminarInmueble(id);
            TempData["SuccessMessage"] = "Inmueble eliminado exitosamente.";
            return RedirectToAction(nameof(ListadoTodosInmuebles));
        }
    }

    // Metodo para mostrar los detalles de un inmueble
    [HttpGet]
    public IActionResult DetallesInmueble(int id)
    {
        //Buscar el Inmueble

        var inmueble = repositorio.ObtenerInmueble(id);

        //Enviar el Contratos
        RepositorioContratos repoContrato = new RepositorioContratos();
        var contrato = repoContrato.ObtenerContratoInmueble(inmueble?.Id_inmueble ?? 0);
        ViewBag.contrato = contrato;

        //Enviar el propietarios
        RepositorioPropietarios repoProp = new RepositorioPropietarios();
        var propietario = repoProp.ObtenerPropietario(inmueble?.Id_propietario ?? 0);
        ViewBag.propietario = propietario;

        //Enviar el Inquilino
        if (contrato != null)
        {
            RepositorioInquilinos repoInquil = new RepositorioInquilinos();
            var inquilino = repoInquil.ObtenerInquilino(contrato.Id_inquilino);
            ViewBag.inquilino = inquilino;
        }

        return View(inmueble);
    }

    // Metodo para buscar inmuebles
    [HttpGet]
    public IActionResult BuscarInmuebles()
    {
        return View();
    }

    // Metodo para buscar inmuebles por id
    [HttpPost]
    public IActionResult BuscarInmueblexId(int id)
    {
        var inmueble = repositorio.ObtenerInmueble(id);
        if (inmueble == null)
        {
            return Json(new { success = false, message = "Inmueble no encontrado" });
        }
        // Enviar la lista de los Contratos
        RepositorioContratos repoContrato = new RepositorioContratos();
        var contratos = repoContrato.ListarContratos();
        ViewBag.contratos = contratos;

        // Enviar la lista de Inquilinos
        RepositorioInquilinos repoInquilino = new RepositorioInquilinos();
        var inquilinos = repoInquilino.ListarInquilinos();
        ViewBag.inquilinos = inquilinos;

        return Json(new { success = true, data = inmueble });
    }

    // Método para buscar inmuebles por dirección
    [HttpPost]
    public IActionResult BuscarInmueblexDir([FromBody] BusquedaInmuebles busqueda)
    {
        if (busqueda == null || string.IsNullOrEmpty(busqueda.Direccion))
        {
            return Json(new { success = false, message = "Dirección no proporcionada" });
        }

        var inmuebles = repositorio.ObtenerInmueblesPorDireccion(busqueda.Direccion);

        return Json(new { success = true, data = inmuebles });
    }

    // Metodo para listar los tipo de inmuebles
    [HttpGet]
    public IActionResult ListarTiposInmueble()
    {
        var lista = repositorio.ListarTiposInmueble();
        return View("ListarTipo", lista);
    }

    // Metodo para crear un nuevo tipo de inmueble
    [HttpGet]
    public IActionResult CrearTipo()
    {
        return View();
    }

    // Metodo para guardar un nuevo tipo de inmueble
    [HttpPost]
    public IActionResult GuardarTipo([FromBody] InmuebleTipo tipo)
    {
        Console.WriteLine($"El tipo es: {tipo}");

        if (ModelState.IsValid)
        {
            try
            {
                repositorio.GuardarTipoInmueble(tipo);
                return Json(new { success = true, message = "Tipo guardado con éxito" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el tipo: {ex.Message}");
                return Json(
                    new { success = false, message = "Error al guardar el tipo de inmueble" }
                );
            }
        }

        return Json(new { success = false, message = "Datos no válidos" });
    }

    // Metodo para Borrar un tipo de inmueble
    [Authorize(Policy = "Administrador")]
    public IActionResult EliminarTipo(int id)
    {
        try
        {
            Console.WriteLine("Eliminando el tipo con ID: " + id);
            // Intentar eliminar el inquilino desde el repositorio
            repositorio.BorrarTipoInmueble(id);
            // Establecer mensaje de éxito
            TempData["SuccessMessage"] = "Tipo eliminado exitosamente.";
        }
        catch (InvalidOperationException ex)
        {
            // Capturar la excepción específica y establecer el mensaje de error
            TempData["ErrorMessage"] = ex.Message;
        }
        catch (Exception ex)
        {
            // Capturar cualquier otra excepción y establecer un mensaje de error genérico
            TempData["ErrorMessage"] = $"Ocurrió un error al eliminar el Tipo. {ex.Message}";
        }

        // Redirigir a la lista de propietarios
        return RedirectToAction(nameof(ListarTiposInmueble));
    }
}
