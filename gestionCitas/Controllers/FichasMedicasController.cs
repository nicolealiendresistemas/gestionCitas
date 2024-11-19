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
    public class FichasMedicasController : Controller
    {
        private readonly GestioncitasContext _context;

        public FichasMedicasController(GestioncitasContext context)
        {
            _context = context;
        }

        // GET: FichasMedicas
        public async Task<IActionResult> Index()
        {
            var gestioncitasContext = _context.FichasMedicas.Include(f => f.Cita);
            return View(await gestioncitasContext.ToListAsync());
        }

        // GET: FichasMedicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fichasMedica = await _context.FichasMedicas
                .Include(f => f.Cita)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fichasMedica == null)
            {
                return NotFound();
            }

            return View(fichasMedica);
        }

        // GET: FichasMedicas/Create
        public IActionResult Create()
        {
            ViewData["CitaId"] = new SelectList(_context.Citas, "Id", "Fecha");
            return View();
        }

        // POST: FichasMedicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CitaId,Diagnostico,Tratamiento,Observaciones")] FichasMedica fichasMedica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fichasMedica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CitaId"] = new SelectList(_context.Citas, "Id", "Id", fichasMedica.CitaId);
            return View(fichasMedica);
        }

        // GET: FichasMedicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fichasMedica = await _context.FichasMedicas.FindAsync(id);
            if (fichasMedica == null)
            {
                return NotFound();
            }
            ViewData["CitaId"] = new SelectList(_context.Citas, "Id", "Id", fichasMedica.CitaId);
            return View(fichasMedica);
        }

        // POST: FichasMedicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CitaId,Diagnostico,Tratamiento,Observaciones")] FichasMedica fichasMedica)
        {
            if (id != fichasMedica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fichasMedica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FichasMedicaExists(fichasMedica.Id))
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
            ViewData["CitaId"] = new SelectList(_context.Citas, "Id", "Id", fichasMedica.CitaId);
            return View(fichasMedica);
        }

        // GET: FichasMedicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fichasMedica = await _context.FichasMedicas
                .Include(f => f.Cita)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fichasMedica == null)
            {
                return NotFound();
            }

            return View(fichasMedica);
        }

        // POST: FichasMedicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fichasMedica = await _context.FichasMedicas.FindAsync(id);
            if (fichasMedica != null)
            {
                _context.FichasMedicas.Remove(fichasMedica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FichasMedicaExists(int id)
        {
            return _context.FichasMedicas.Any(e => e.Id == id);
        }
    }
}
