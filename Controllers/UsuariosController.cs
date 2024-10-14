using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Inmobiliaria.Repositorios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Org.BouncyCastle.Bcpg;

namespace Inmobiliaria.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;

        private readonly RepositorioUsuarios? repositorio;

        public UsuariosController(
            ILogger<UsuariosController> logger,
            IConfiguration configuration,
            IWebHostEnvironment environment
        )
        {
            _logger = logger;
            repositorio = new RepositorioUsuarios();
            this.configuration = configuration;
            this.environment = environment;
        }

        // Método para listar los usuarios
        [Authorize(Policy = "Administrador")]
        [HttpGet]
        public IActionResult ListadoUsuarios()
        {
            var lista = repositorio.ListarUsuarios();
            return View(lista);
        }

        // Método para obtener los detalles de un usuario por ID
        [Authorize(Policy = "Administrador")]
        [HttpGet]
        public IActionResult DetalleUsuario(int id)
        {
            var usuario = repositorio.ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                TempData["ErrorMessage"] = "El usuario no existe. Intente nuevamente.";
            }
            return View(usuario);
        }

        // Método para la vista crear un nuevo usuario
        [Authorize(Policy = "Administrador")]
        [HttpGet]
        public IActionResult CrearUsuario()
        {
            return View();
        }

        // Método para crear un nuevo usuario
        [Authorize(Policy = "Administrador")]
        [HttpPost]
        public IActionResult NuevoUsuario(Usuario usuario)
        {
            var existe = repositorio.ExisteUsuario(usuario.Email);

            // Verificar si existe el usuario
            if (existe)
            {
                TempData["ErrorMessage"] =
                    "Ocurrió un error al crear el usuario. El Correo ya esta registrado.";
                return View(usuario);
            }
            // Verifica si el modelo es válido
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Modelo inválido");
                TempData["ErrorMessage"] =
                    "Ocurrió un error al crear el usuario. Intente nuevamente.";
                return View(usuario);
            }

            try
            {
                // Hashea la contraseña usando un algoritmo seguro
                string hashed = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: usuario.Password,
                        salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8
                    )
                );

                // Asigna la contraseña hasheada al usuario
                usuario.Password = hashed;

                // Genera un nuevo nombre aleatorio para el avatar (opcional)
                var nbreRnd = Guid.NewGuid(); // Puedes usar este nombre si necesitas aleatoriedad

                // Verifica si se ha subido un archivo de avatar
                if (usuario.AvatarFile != null)
                {
                    // Obtiene la ruta donde se guardará el avatar
                    string wwwPath = environment.WebRootPath;
                    string path = Path.Combine(wwwPath, "Uploads");

                    // Crea el directorio si no existe
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // Genera un nombre de archivo único para el avatar usando el ID del usuario
                    string fileName =
                        "avatar_"
                        + usuario.Nombre
                        + nbreRnd
                        + Path.GetExtension(usuario.AvatarFile.FileName);
                    string pathCompleto = Path.Combine(path, fileName);
                    usuario.Avatar = Path.Combine("/Uploads", fileName); // Guarda la ruta relativa del avatar

                    // Guarda el archivo del avatar en la ruta especificada
                    using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                    {
                        usuario.AvatarFile.CopyTo(stream);
                    }
                }
                // Guarda el nuevo usuario en la base de datos
                int res = repositorio.CrearUsuario(usuario);

                // Redirige a la acción Index después de crear el usuario
                return RedirectToAction(nameof(ListadoUsuarios));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear el usuario: {ex.Message}");
                TempData["ErrorMessage"] =
                    "Ocurrió un error al crear el usuario. Intente nuevamente.";
                return View(usuario);
            }
        }

        // Método para editar un usuario
        [HttpGet]
        public IActionResult EditarUsuario(int id)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var UserId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var empleadoId = Convert.ToInt32(UserId);

            if (userRole == "Administrador")
            {
                var usuario = repositorio.ObtenerUsuarioPorId(id);
                return View(usuario);
            }
            else if (userRole == "Empleado")
            {
                if (empleadoId != id)
                {
                    var usuario = repositorio.ObtenerUsuarioPorId(empleadoId);
                    TempData["ErrorMessage"] =
                        "No tiene permisos para editar este usuario. Se redirecciona a su usuario.";
                    return View(usuario);
                }
                else
                {
                    var usuario = repositorio.ObtenerUsuarioPorId(empleadoId);
                    return View(usuario);
                }
            }
            return View("Login");
        }

        // Método para Modificar un usuario
        [HttpPost]
        public IActionResult ModificarUser(Usuario user)
        {
            // Verifica si el modelo es válido
            if (ModelState.IsValid)
            {
                try
                {
                    // Si se ha subido un archivo de avatar
                    if (user.AvatarFile != null)
                    {
                        // Obtiene la ruta donde se guardará el avatar
                        string wwwPath = environment.WebRootPath;
                        string path = Path.Combine(wwwPath, "Uploads");

                        // Crea el directorio si no existe
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        // Genera un nuevo nombre aleatorio para el avatar (opcional)
                        var nbreRnd = Guid.NewGuid(); // Puedes usar este nombre si necesitas aleatoriedad

                        // Genera un nombre de archivo único para el avatar
                        string fileName =
                            "avatar_"
                            + user.Id_usuario
                            + user.Nombre
                            + nbreRnd
                            + Path.GetExtension(user.AvatarFile.FileName);
                        string pathCompleto = Path.Combine(path, fileName);

                        // Guarda la ruta relativa del avatar en el campo Avatar
                        user.Avatar = Path.Combine("/Uploads", fileName).Replace("\\", "/");

                        // Guarda el archivo del avatar en la ruta especificada
                        using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                        {
                            user.AvatarFile.CopyTo(stream);
                        }
                    }

                    // Llama al repositorio para actualizar el usuario en la base de datos
                    int resultado = repositorio.ActualizarUsuario(user);

                    // Verifica si la actualización fue exitosa
                    if (resultado > 0)
                    {
                        var userRole = User
                            .Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)
                            ?.Value;
                        //Si es Administrador
                        if (userRole == "Administrador")
                        {
                            TempData["SuccessMessage"] = "Usuario actualizado correctamente.";
                            return RedirectToAction("ListadoUsuarios");
                        }
                        else if (userRole == "Empleado")
                        {
                            TempData["SuccessMessage"] = "Usuario actualizado correctamente.";
                            return RedirectToAction("EditarUsuario", new { id = user.Id_usuario });
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] =
                            "Ocurrió un error al actualizar el usuario. Intente nuevamente.";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al actualizar el usuario: {ex.Message}");
                    TempData["ErrorMessage"] =
                        "Ocurrió un error al actualizar el usuario. Intente nuevamente.";
                }
            }

            // Si el modelo no es válido, regresa a la vista de edición
            return View("EditarUsuario", user);
        }

        [Authorize(Policy = "Administrador")]
        // Método para eliminar un usuario
        public IActionResult EliminarUsuario(int id)
        {
            try
            {
                repositorio.EliminarUsuario(id);
                TempData["SuccessMessage"] = "Usuario eliminado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al eliminar el usuario.";
            }

            return RedirectToAction("ListadoUsuarios");
        }

        // Eliminar Avatar
        public IActionResult EliminarAvatar(int id)
        {
            try
            {
                // Llama al método del repositorio para borrar el avatar
                repositorio.BorrarAvatar(id);
                TempData["SuccessMessage"] = "Avatar eliminado correctamente.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el Avatar: {ex.Message}");
                TempData["ErrorMessage"] = "Error al eliminar el Avatar.";
            }

            // Redirige a la acción de editar usuario
            return RedirectToAction("EditarUsuario", new { id });
        }

        //------------------------------------------------------------LOGIN------------------------------------------------------------

        // Acceso a LoginIn
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Método para procesar el formulario de Login
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginIn(LoginView login)
        {
            try
            {
                // Redireccionamiento después del login, si no hay url de retorno se va a Home
                var returnUrl = String.IsNullOrEmpty(TempData["returnUrl"] as string)
                    ? "/Home"
                    : TempData["returnUrl"].ToString();

                if (ModelState.IsValid)
                {
                    // Hash de la contraseña utilizando el salt del archivo de configuración
                    string hashedPassword = Convert.ToBase64String(
                        KeyDerivation.Pbkdf2(
                            password: login.Password,
                            salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]), // Salt almacenado en config
                            prf: KeyDerivationPrf.HMACSHA1,
                            iterationCount: 1000,
                            numBytesRequested: 256 / 8
                        )
                    );

                    // Obtener usuario por email
                    var usuario = repositorio.ObtenerPorEmail(login.Email); // Crear un repositorio

                    // Verificar si el usuario existe y la contraseña es correcta
                    if (usuario == null || usuario.Password != hashedPassword)
                    {
                        // Si el usuario no existe o la contraseña no coincide, mostrar error
                        ModelState.AddModelError("", "El email o la contraseña no son correctos");
                        TempData["ErrorMessage"] = "El email o la contraseña no son correctos.";
                        TempData["returnUrl"] = returnUrl;
                        return View("Login", login);
                    }

                    // Crear los claims (roles, email, nombre completo)
                    var claims = new List<Claim>
                    {
                        new Claim("UserId", usuario.Id_usuario.ToString()),
                        new Claim(ClaimTypes.Name, usuario.Email),
                        new Claim("FullName", usuario.Nombre + " " + usuario.Apellido),
                        new Claim(ClaimTypes.Role, usuario.Rol.ToString())
                    };

                    // Crear identidad con los claims y el esquema de autenticación de cookies
                    var claimsIdentity = new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme
                    );

                    // Iniciar sesión utilizando cookies
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity)
                    );

                    // Limpiar TempData y edirigir a lar URL de retorno
                    TempData.Remove("returnUrl");
                    return View("InicioLogin");
                }

                // Si no es válido el modelo, volver a la vista de login
                TempData["returnUrl"] = returnUrl;
                TempData["ErrorMessage"] = "El email o la contraseña no son correctos.";
                return View("Login", login);
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                ModelState.AddModelError(
                    "",
                    "Ocurrió un error durante el proceso de login: " + ex.Message
                );
                return View("Login", login);
            }
        }

        // Método para cerrar sesión
        [Route("salir", Name = "logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // Metodo para cambiar el Password
        [Authorize]
        [HttpPost]
        public IActionResult CambiarPass([FromBody] CambioPass pass)
        {
            try
            {
                // Obtener el usuario y verificar el id
                var usuario = repositorio.ObtenerUsuarioPorId(pass.Id_usuario);
                if (usuario == null)
                {
                    return Json(new { success = false, message = "Usuario no encontrado" });
                }

                // Verificar si las contraseñas coinciden
                if (pass.Password != pass.ConfirmPassword)
                {
                    return Json(new { success = false, message = "Las contraseñas no coinciden" });
                }

                // Hashear la nueva contraseña
                string hashedNewPassword = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: pass.Password,
                        salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8
                    )
                );

                // Actualizar la contraseña en la base de datos
                repositorio.CambiarPassword(pass.Id_usuario, hashedNewPassword);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return Json(
                    new
                    {
                        success = false,
                        message = "Error al actualizar la contraseña: " + ex.Message
                    }
                );
            }
        }

        // Método para blanquear la contraseña
        [Authorize]
        [HttpPost]
        public IActionResult BlanquearPass([FromBody] CambioPass pass)
        {
            try
            {
                // Obtener el usuario y verificar el id
                var usuario = repositorio.ObtenerUsuarioPorId(pass.Id_usuario);
                if (usuario == null)
                {
                    return Json(new { success = false, message = "Usuario no encontrado" });
                }

                // Hashear la nueva contraseña
                string passBlanca = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: pass.Email,
                        salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8
                    )
                );

                // Actualizar la contraseña en la base de datos
                repositorio.CambiarPassword(pass.Id_usuario, passBlanca);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return Json(
                    new
                    {
                        success = false,
                        message = "Error al actualizar la contraseña: " + ex.Message
                    }
                );
            }
        }
    }
}
