using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoGYM.Data;
using ProyectoGYM.Models;

namespace ProyectoGYM.Controllers
{
    public class JornadasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JornadasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jornadas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Jornadas.Include(j => j.IdempleadoNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Jornadas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jornada = await _context.Jornadas
                .Include(j => j.IdempleadoNavigation)
                .FirstOrDefaultAsync(m => m.Idjornadas == id);
            if (jornada == null)
            {
                return NotFound();
            }

            return View(jornada);
        }

        // GET: Jornadas/Create
        public IActionResult Create()
        {
            ViewData["Idempleado"] = new SelectList(_context.Empleados, "Idempleado", "Apellidos");
            return View();
        }

        // POST: Jornadas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idjornadas,Idempleado,FechaInicio,FechaFin,HorasTrabajadas")] Jornada jornada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jornada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idempleado"] = new SelectList(_context.Empleados, "Idempleado", "Apellidos", jornada.Idempleado);
            return View(jornada);
        }

        // GET: Jornadas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jornada = await _context.Jornadas.FindAsync(id);
            if (jornada == null)
            {
                return NotFound();
            }
            ViewData["Idempleado"] = new SelectList(_context.Empleados, "Idempleado", "Apellidos", jornada.Idempleado);
            return View(jornada);
        }

        // POST: Jornadas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idjornadas,Idempleado,FechaInicio,FechaFin,HorasTrabajadas")] Jornada jornada)
        {
            if (id != jornada.Idjornadas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jornada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JornadaExists(jornada.Idjornadas))
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
            ViewData["Idempleado"] = new SelectList(_context.Empleados, "Idempleado", "Apellidos", jornada.Idempleado);
            return View(jornada);
        }

        // GET: Jornadas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jornada = await _context.Jornadas
                .Include(j => j.IdempleadoNavigation)
                .FirstOrDefaultAsync(m => m.Idjornadas == id);
            if (jornada == null)
            {
                return NotFound();
            }

            return View(jornada);
        }

        // POST: Jornadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jornada = await _context.Jornadas.FindAsync(id);
            if (jornada != null)
            {
                _context.Jornadas.Remove(jornada);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JornadaExists(int id)
        {
            return _context.Jornadas.Any(e => e.Idjornadas == id);
        }
    }
}
