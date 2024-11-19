using gestionCitas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gestionCitas.Controllers
{
    public class TicketsController : Controller
    {
        private readonly GestioncitasContext _context;

        public TicketsController(GestioncitasContext context)
        {
            _context = context;
        }

        // Generar un ticket para un paciente
        [HttpPost]
        public async Task<IActionResult> Create(int pacienteId)
        {
            var ticket = new Ticket
            {
                PacienteId = pacienteId
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Ticket #{ticket.Id} generado exitosamente.";
            return RedirectToAction("Index", "Pacientes");
        }

        // Reporte de tickets para recepcionistas
        public async Task<IActionResult> Reporte()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Paciente)
                .OrderBy(t => t.Id) // Orden por ID (orden de llegada)
                .ToListAsync();

            return View(tickets);
        }
    }
}
