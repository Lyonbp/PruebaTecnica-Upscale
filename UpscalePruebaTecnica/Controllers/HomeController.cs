using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UpscalePruebaTecnica.Models;

namespace UpscalePruebaTecnica.Controllers
{
    public class HomeController : Controller
    {
        private readonly UpscaleDbContext _context;

        public HomeController(UpscaleDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard(int id)
        {
            // Si por error entran sin ID, los mandamos al login
            if (id == 0) return RedirectToAction("Index", "Login");

            // Buscamos al usuario y traemos sus datos de TipoDocumento y Contratación
            var usuario = await _context.Usuarios
                .Include(u => u.TipoDocumento)
                .Include(u => u.TipoContratacion)
                .FirstOrDefaultAsync(u => u.UsuarioId == id);

            if (usuario == null) return RedirectToAction("Index", "Login");

            // Le pasamos el modelo (usuario) a la vista HTML
            return View(usuario);
        }
    }
}
