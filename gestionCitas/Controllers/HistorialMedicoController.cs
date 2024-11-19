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
    [Authorize(Roles = "Administrador,Doctor")]
    public class HistorialMedicoController : Controller
    {
        private readonly GestioncitasContext _context;

        public HistorialMedicoController(GestioncitasContext context)
        {
            _context = context;
        }

        // GET: HistorialMedico
        public async Task<IActionResult> Index()
        {
            var gestioncitasContext = _context.HistorialMedicos.Include(h => h.Consulta);
            return View(await gestioncitasContext.ToListAsync());
        }

        // GET: HistorialMedico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialMedico = await _context.HistorialMedicos
                .Include(h => h.Consulta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historialMedico == null)
            {
                return NotFound();
            }

            return View(historialMedico);
        }

        // GET: HistorialMedico/Create
        public IActionResult Create()
        {
            ViewData["ConsultaId"] = new SelectList(_context.Consultas, "Id", "FechaConsulta");
            return View();
        }

        // POST: HistorialMedico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConsultaId,Diagnostico,Tratamiento,Observaciones,FechaRegistro")] HistorialMedico historialMedico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialMedico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultaId"] = new SelectList(_context.Consultas, "Id", "FechaConsulta", historialMedico.ConsultaId);
            return View(historialMedico);
        }

        // GET: HistorialMedico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialMedico = await _context.HistorialMedicos.FindAsync(id);
            if (historialMedico == null)
            {
                return NotFound();
            }
            ViewData["ConsultaId"] = new SelectList(_context.Consultas, "Id", "Id", historialMedico.ConsultaId);
            return View(historialMedico);
        }

        // POST: HistorialMedico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConsultaId,Diagnostico,Tratamiento,Observaciones,FechaRegistro")] HistorialMedico historialMedico)
        {
            if (id != historialMedico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialMedico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialMedicoExists(historialMedico.Id))
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
            ViewData["ConsultaId"] = new SelectList(_context.Consultas, "Id", "Id", historialMedico.ConsultaId);
            return View(historialMedico);
        }

        // GET: HistorialMedico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialMedico = await _context.HistorialMedicos
                .Include(h => h.Consulta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historialMedico == null)
            {
                return NotFound();
            }

            return View(historialMedico);
        }

        // POST: HistorialMedico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historialMedico = await _context.HistorialMedicos.FindAsync(id);
            if (historialMedico != null)
            {
                _context.HistorialMedicos.Remove(historialMedico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialMedicoExists(int id)
        {
            return _context.HistorialMedicos.Any(e => e.Id == id);
        }
    }
}
