using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UpscalePruebaTecnica.Models;

namespace UpscalePruebaTecnica.Controllers
{
    public class LoginController : Controller
    {
        private readonly UpscaleDbContext _context;

        // Inyección del DbContext
        public LoginController(UpscaleDbContext context)
        {
            _context = context;
        }

        // GET: Muestra la vista de Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: Procesa las credenciales
        [HttpPost]
        public async Task<IActionResult> Index(string tipoDocumento, string numeroDocumento, string password)
        {
            // Validar que vengan los datos
            if (string.IsNullOrEmpty(tipoDocumento) || string.IsNullOrEmpty(numeroDocumento) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Por favor, complete todos los campos.";
                return View();
            }

            // 2. Buscar al usuario
            var usuario = await _context.Usuarios
                .Include(u => u.TipoDocumento)
                .FirstOrDefaultAsync(u => u.NumeroDocumento == numeroDocumento && u.TipoDocumento.Abreviatura == tipoDocumento);

            if (usuario == null)
            {
                ViewBag.Error = "Usuario incorrecto";
                return View();
            }

            // Verificar si la cuenta está bloqueada
            if (usuario.FechaBloqueo.HasValue)
            {
                var minutosBloqueado = (DateTime.Now - usuario.FechaBloqueo.Value).TotalMinutes;
                if (minutosBloqueado < 15)
                {
                    // Aún no pasan los 15 minutos
                    return RedirectToAction("CuentaBloqueada");
                }
                else
                {
                    // Ya pasaron, desbloquear cuenta
                    usuario.IntentosFallidos = 0;
                    usuario.FechaBloqueo = null;
                    await _context.SaveChangesAsync();
                }
            }

            //Validar contraseña
            if (usuario.PasswordHash != password)
            {
                usuario.IntentosFallidos++;

                if (usuario.IntentosFallidos >= 5)
                {
                    usuario.FechaBloqueo = DateTime.Now;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("CuentaBloqueada");
                }

                await _context.SaveChangesAsync();
                ViewBag.Error = "Contraseña incorrecta";

                ViewBag.Intentos = usuario.IntentosFallidos;
                return View();
            }

            //Login Exitoso
            usuario.IntentosFallidos = 0;
            usuario.FechaBloqueo = null;
            await _context.SaveChangesAsync();

            return RedirectToAction("Dashboard", "Home", new { id = usuario.UsuarioId });
        }

        // GET: Vista de Cuenta Bloqueada
        [HttpGet]
        public IActionResult CuentaBloqueada()
        {
            return View();
        }
    }
}
