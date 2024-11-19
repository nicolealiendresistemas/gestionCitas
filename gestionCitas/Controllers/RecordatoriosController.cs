using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestionCitas.Models;
using Microsoft.AspNetCore.Authorization;

namespace gestionCitas.Controllers
{
    [Authorize(Roles = "Administrador,Recepcionista")]
    public class RecordatoriosController : Controller
    {
        private readonly GestioncitasContext _context;

        public RecordatoriosController(GestioncitasContext context)
        {
            _context = context;
        }

        // GET: Recordatorios
        public async Task<IActionResult> Index()
        {
            var gestioncitasContext = _context.Recordatorios.Include(r => r.Cita);
            return View(await gestioncitasContext.ToListAsync());
        }

        // GET: Recordatorios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordatorio = await _context.Recordatorios
                .Include(r => r.Cita)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recordatorio == null)
            {
                return NotFound();
            }

            return View(recordatorio);
        }

        // GET: Recordatorios/Create
        public IActionResult Create()
        {
            ViewData["CitaId"] = new SelectList(_context.Citas, "Id", "Fecha");
            return View();
        }

        // POST: Recordatorios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CitaId,FechaEnvio,MetodoEnvio,Mensaje")] Recordatorio recordatorio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recordatorio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CitaId"] = new SelectList(_context.Citas, "Id", "Fecha", recordatorio.CitaId);
            return View(recordatorio);
        }

        // GET: Recordatorios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordatorio = await _context.Recordatorios.FindAsync(id);
            if (recordatorio == null)
            {
                return NotFound();
            }
            ViewData["CitaId"] = new SelectList(_context.Citas, "Id", "Fecha", recordatorio.CitaId);
            return View(recordatorio);
        }

        // POST: Recordatorios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CitaId,FechaEnvio,MetodoEnvio,Mensaje")] Recordatorio recordatorio)
        {
            if (id != recordatorio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recordatorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordatorioExists(recordatorio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CitaId"] = new SelectList(_context.Citas, "Id", "Fecha", recordatorio.CitaId);
            return View(recordatorio);
        }

        // GET: Recordatorios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordatorio = await _context.Recordatorios
                .Include(r => r.Cita)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recordatorio == null)
            {
                return NotFound();
            }

            return View(recordatorio);
        }

        // POST: Recordatorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recordatorio = await _context.Recordatorios.FindAsync(id);
            if (recordatorio != null)
            {
                _context.Recordatorios.Remove(recordatorio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordatorioExists(int id)
        {
            return _context.Recordatorios.Any(e => e.Id == id);
        }
    }
}
