using Microsoft.AspNetCore.Mvc;
using gestionCitas.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace gestionCitas.Controllers
{
    public class CarritoCitasController : Controller
    {
        private const string CarritoCookieKey = "CarritoCitas";
        private readonly GestioncitasContext _context;

        public CarritoCitasController(GestioncitasContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var carritoIds = Request.Cookies["CarritoCitas"];
            List<Cita> carrito = new List<Cita>();

            if (!string.IsNullOrEmpty(carritoIds))
            {
                var ids = carritoIds.Split(',').Select(int.Parse).ToList();
                carrito = _context.Citas
                    .Where(c => ids.Contains(c.Id))
                    .Include(c => c.Paciente)
                    .Include(c => c.Medico)
                    .ToList();
            }

            return View(carrito);
        }


        [HttpPost]
        public IActionResult AgregarCita(int citaId)
        {
            var carritoIds = Request.Cookies[CarritoCookieKey];
            List<int> ids = string.IsNullOrEmpty(carritoIds)
                ? new List<int>()
                : carritoIds.Split(',').Select(int.Parse).ToList();

            if (!ids.Contains(citaId))
            {
                ids.Add(citaId);
                Response.Cookies.Append(CarritoCookieKey, string.Join(",", ids), new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(1) // La cookie expirará en 1 día
                });
            }

            // Redirige a la página actual para actualizar el carrito
            return RedirectToAction("Index", "Citas");
        }


        [HttpPost]
        public IActionResult VaciarCarrito()
        {
            Response.Cookies.Delete(CarritoCookieKey);
            return RedirectToAction("Index", "Citas");
        }
    }
}
