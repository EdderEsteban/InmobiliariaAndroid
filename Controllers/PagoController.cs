using Inmobiliaria.Models;
using Inmobiliaria.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Inmobiliaria.Controllers
{
    [Authorize]
    public class PagoController : Controller
    {
        private readonly ILogger<PagoController> _logger;
        private readonly RepositorioPago repositorio;

        public PagoController(ILogger<PagoController> logger)
        {
            _logger = logger;
            repositorio = new RepositorioPago();
        }

        [HttpGet]
        public IActionResult ListadoPagos()
        {
            var lista = repositorio.ListarPagos();
            ViewBag.inmuebles = new RepositorioInmuebles().ListarTodosInmuebles();
            ViewBag.inquilinos = new RepositorioInquilinos().ListarInquilinos();
            ViewBag.contratos = new RepositorioContratos().ListarContratos();

            return View(lista);
        }

        [HttpGet]
        public IActionResult BuscarInquilinoPago()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CrearPago(int idInquilino, int idContrato)
        {
            // Obtener el contrato por ID
            var contrato = new RepositorioContratos().ObtenerContrato(idContrato);
            if (contrato == null)
            {
                return NotFound(); // Retorna un 404 si no se encuentra el contrato
            }

            // Obtener el inmueble asociado al contrato
            var inmueble = new RepositorioInmuebles().ObtenerInmueble(contrato.Id_inmueble);
            if (inmueble == null)
            {
                return NotFound(); // Retorna un 404 si no se encuentra el inmueble
            }

            ViewBag.Inmueble = inmueble; // Pasar el inmueble al ViewBag
            ViewBag.Contrato = contrato; // Pasar el contrato al ViewBag

            // Obtener el inquilino asociado al contrato
            var inquilino = new RepositorioInquilinos().ObtenerInquilino(idInquilino);
            if (inquilino != null)
            {
                ViewBag.Inquilino = inquilino; // Pasar el inquilino al ViewBag si se encuentra
            }

            return View();
        }

        [HttpPost]
        public IActionResult GuardarPago(Pago pago)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pagado = repositorio.ExistePago(pago);

                    if (!pagado) // Solo guarda si el pago no existe
                    {
                        // Asigna el Usuario que creo el registro
                        var UserId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                        pago.Id_Usuario = int.Parse(UserId);
                        
                        repositorio.GuardarNuevo(pago);
                        TempData["SuccessMessage"] = "El pago se guardó exitosamente."; // Mensaje de éxito
                        return RedirectToAction(nameof(ListadoPagos));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Ya hay un pago registrado para este periodo."; // Mensaje de éxito
                        return RedirectToAction(nameof(CrearPago), new { idInquilino = pago.Id_Inquilino, idContrato = pago.Id_Contrato });
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al guardar el pago.");
                    ModelState.AddModelError(
                        "",
                        "Ocurrio un error al guardar el pago. Intente nuevamente."
                    );
                }
            }

            // Si el modelo no es válido o hubo un error, regresa a la vista CrearPago con el pago actual
            return View("CrearPago", pago);
        }

        [HttpGet]
        public IActionResult EditarPago(int id)
        {
            var pago = repositorio.ObtenerPago(id);
            if (pago == null)
            {
                return NotFound(); // Retorna un 404 si no se encuentra el pago
            }

            ViewBag.contratos = new RepositorioContratos().ListarContratos();
            ViewBag.inquilinos = new RepositorioInquilinos().ListarInquilinos();
            return View(pago);
        }

        /* No lo uso porq no considero necesario actualizar un pago, si hay un error, deberia quedar registrado
        [HttpPost]
         public IActionResult ActualizarPago(Pago pago)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     repositorio.ActualizarPago(pago);
                     TempData["SuccessMessage"] = "Pago actualizado exitosamente.";
                     return RedirectToAction(nameof(ListadoPagos));
                 }
                 catch (Exception ex)
                 {
                     _logger.LogError(ex, "Error al actualizar el pago.");
                     ModelState.AddModelError(
                         "",
                         "Ocurrió un error al actualizar el pago. Intente nuevamente."
                     );
                 }
             }
             return View("EditarPago", pago);
         }*/

        [Authorize(Policy = "Administrador")]
        public IActionResult EliminarPago(int id)
        {
            try
            {
                Console.WriteLine("Eliminando el pago con ID: " + id);
                repositorio.EliminarPago(id);
                TempData["SuccessMessage"] = "Pago eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el pago.");
                TempData["ErrorMessage"] =
                    "Ocurrió un error al eliminar el pago. Intente nuevamente.";
            }
            return RedirectToAction(nameof(ListadoPagos));
        }

        [HttpGet]
        public IActionResult DetallesPago(int id)
        {
            // Obtener el pago
            var detalle = repositorio.ObtenerPago(id);
            // Obtener el inquilino
            var inquilino = new RepositorioInquilinos().ObtenerInquilino(detalle.Id_Inquilino);
            // Obtener el contrato
            var contrato = new RepositorioContratos().ObtenerContrato(detalle.Id_Contrato);
            // Obtener el inmueble
            var inmueble = new RepositorioInmuebles().ObtenerInmueble(contrato.Id_inmueble);
            // Enviar a la vista
            ViewBag.inquilino = inquilino;
            ViewBag.contrato = contrato;
            ViewBag.inmueble = inmueble;

            return View(detalle);
        }

        [HttpPost]
        public IActionResult BuscarInqPago([FromBody] BusquedaInquilinos busqueda)
        {
            // Realiza la búsqueda de los inquilinos
            var inquilinos = new RepositorioInquilinos().BuscarInquilinos(busqueda);

            // Itera sobre los inquilinos encontrados y busca los contratos e inmuebles asociados para cada uno
            var resultadoConContratosEInmuebles = inquilinos
                .Select(i =>
                {
                    // Obtener todos los contratos del inquilino
                    var contratos = new RepositorioContratos().ObtenerContratosPorInquilino(
                        i.Id_inquilino
                    );

                    // Crea una lista para almacenar los datos de contratos e inmuebles
                    var contratosEInmuebles = new List<object>();

                    foreach (var contrato in contratos)
                    {
                        // Obtener el inmueble asociado a cada contrato
                        var inmueble = new RepositorioInmuebles().ObtenerInmueble(
                            contrato.Id_inmueble
                        );

                        // Agrega los datos a la lista
                        contratosEInmuebles.Add(
                            new
                            {
                                Id_Contrato = contrato.Id_contrato,
                                Id_Inmueble = contrato.Id_inmueble,
                                Fecha_Inicio = contrato.Fecha_inicio,
                                Fecha_Fin = contrato.Fecha_fin,
                                Inmueble = inmueble != null
                                    ? new
                                    {
                                        Id_Inmueble = inmueble.Id_inmueble,
                                        inmueble.Direccion,
                                    }
                                    : null // Si no hay inmueble, será null
                            }
                        );
                    }

                    // Retorna un objeto que contiene la información del inquilino y sus contratos e inmuebles
                    return new
                    {
                        Id_Inquilino = i.Id_inquilino,
                        Nombre = i.Nombre,
                        Apellido = i.Apellido,
                        Dni = i.Dni,
                        ContratosEInmuebles = contratosEInmuebles // Lista de contratos e inmuebles
                    };
                })
                .Where(x => x.ContratosEInmuebles.Any()) // Filtra solo los que tienen contratos
                .ToList();

            // Devuelve los resultados como JSON
            return Json(resultadoConContratosEInmuebles);
        }
    }
}
