using gestionCitas.Models;
using Microsoft.AspNetCore.Mvc;
using gestionCitas.Extensions;



namespace gestionCitas.Controllers
{
    public class CarritoCitasController : Controller
    {
        private const string CarritoSessionKey = "CarritoCitas";

        public IActionResult Index()
        {
            var carrito = ObtenerCarritoDeSesion();
            return View(carrito);
        }

        [HttpPost]
        public IActionResult AgregarCita(CarritoCitasItem cita)
        {
            var carrito = ObtenerCarritoDeSesion();
            carrito.Add(cita);
            GuardarCarritoEnSesion(carrito);
            return RedirectToAction("Index", "Citas");
        }

        [HttpPost]
        public IActionResult VaciarCarrito()
        {
            HttpContext.Session.Remove(CarritoSessionKey);
            return RedirectToAction("Index", "Citas");
        }

        private List<CarritoCitasItem> ObtenerCarritoDeSesion()
        {
            var carrito = HttpContext.Session.GetObject<List<CarritoCitasItem>>(CarritoSessionKey);
            return carrito ?? new List<CarritoCitasItem>();
        }

        private void GuardarCarritoEnSesion(List<CarritoCitasItem> carrito)
        {
            HttpContext.Session.SetObject(CarritoSessionKey, carrito);
        }
    }
}
