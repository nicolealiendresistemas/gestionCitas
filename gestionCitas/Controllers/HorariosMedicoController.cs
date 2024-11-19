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
    public class HorariosMedicoController : Controller
    {
        private readonly GestioncitasContext _context;

        public HorariosMedicoController(GestioncitasContext context)
        {
            _context = context;
        }

        // GET: HorariosMedico
        public async Task<IActionResult> Index()
        {
            var gestioncitasContext = _context.HorariosMedicos.Include(h => h.Medico);
            return View(await gestioncitasContext.ToListAsync());
        }

        // GET: HorariosMedico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horariosMedico = await _context.HorariosMedicos
                .Include(h => h.Medico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horariosMedico == null)
            {
                return NotFound();
            }

            return View(horariosMedico);
        }

        // GET: HorariosMedico/Create
        public IActionResult Create()
        {
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Nombre");
            return View();
        }

        // POST: HorariosMedico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MedicoId,DiaSemana,HoraInicio,HoraFin")] HorariosMedico horariosMedico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horariosMedico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Nombre", horariosMedico.MedicoId);
            return View(horariosMedico);
        }

        // GET: HorariosMedico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horariosMedico = await _context.HorariosMedicos.FindAsync(id);
            if (horariosMedico == null)
            {
                return NotFound();
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Id", horariosMedico.MedicoId);
            return View(horariosMedico);
        }

        // POST: HorariosMedico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedicoId,DiaSemana,HoraInicio,HoraFin")] HorariosMedico horariosMedico)
        {
            if (id != horariosMedico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horariosMedico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorariosMedicoExists(horariosMedico.Id))
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
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Id", horariosMedico.MedicoId);
            return View(horariosMedico);
        }

        // GET: HorariosMedico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horariosMedico = await _context.HorariosMedicos
                .Include(h => h.Medico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horariosMedico == null)
            {
                return NotFound();
            }

            return View(horariosMedico);
        }

        // POST: HorariosMedico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horariosMedico = await _context.HorariosMedicos.FindAsync(id);
            if (horariosMedico != null)
            {
                _context.HorariosMedicos.Remove(horariosMedico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorariosMedicoExists(int id)
        {
            return _context.HorariosMedicos.Any(e => e.Id == id);
        }
    }
}
