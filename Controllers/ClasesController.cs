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
    public class ClasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clases
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Clases.Include(c => c.IdactividadNavigation).Include(c => c.IdempleadoNavigation).Include(c => c.IdmatriculaNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Clases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clases
                .Include(c => c.IdactividadNavigation)
                .Include(c => c.IdempleadoNavigation)
                .Include(c => c.IdmatriculaNavigation)
                .FirstOrDefaultAsync(m => m.Idclase == id);
            if (clase == null)
            {
                return NotFound();
            }

            return View(clase);
        }

        // GET: Clases/Create
        public IActionResult Create()
        {
            ViewData["Idactividad"] = new SelectList(_context.Actividades, "Idactividades", "Descripcion");
            ViewData["Idempleado"] = new SelectList(_context.Empleados, "Idempleado", "Apellidos");
            ViewData["Idmatricula"] = new SelectList(_context.Martriculas, "Idmatricula", "Idmatricula");
            return View();
        }

        // POST: Clases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idclase,Idmatricula,Idactividad,Idempleado,FechaInicio,FechaFinal,HoraInicio,HoraFin,Estado,Capacidad")] Clase clase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idactividad"] = new SelectList(_context.Actividades, "Idactividades", "Descripcion", clase.Idactividad);
            ViewData["Idempleado"] = new SelectList(_context.Empleados, "Idempleado", "Apellidos", clase.Idempleado);
            ViewData["Idmatricula"] = new SelectList(_context.Martriculas, "Idmatricula", "Idmatricula", clase.Idmatricula);
            return View(clase);
        }

        // GET: Clases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clases.FindAsync(id);
            if (clase == null)
            {
                return NotFound();
            }
            ViewData["Idactividad"] = new SelectList(_context.Actividades, "Idactividades", "Descripcion", clase.Idactividad);
            ViewData["Idempleado"] = new SelectList(_context.Empleados, "Idempleado", "Apellidos", clase.Idempleado);
            ViewData["Idmatricula"] = new SelectList(_context.Martriculas, "Idmatricula", "Idmatricula", clase.Idmatricula);
            return View(clase);
        }

        // POST: Clases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idclase,Idmatricula,Idactividad,Idempleado,FechaInicio,FechaFinal,HoraInicio,HoraFin,Estado,Capacidad")] Clase clase)
        {
            if (id != clase.Idclase)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaseExists(clase.Idclase))
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
            ViewData["Idactividad"] = new SelectList(_context.Actividades, "Idactividades", "Descripcion", clase.Idactividad);
            ViewData["Idempleado"] = new SelectList(_context.Empleados, "Idempleado", "Apellidos", clase.Idempleado);
            ViewData["Idmatricula"] = new SelectList(_context.Martriculas, "Idmatricula", "Idmatricula", clase.Idmatricula);
            return View(clase);
        }

        // GET: Clases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clases
                .Include(c => c.IdactividadNavigation)
                .Include(c => c.IdempleadoNavigation)
                .Include(c => c.IdmatriculaNavigation)
                .FirstOrDefaultAsync(m => m.Idclase == id);
            if (clase == null)
            {
                return NotFound();
            }

            return View(clase);
        }

        // POST: Clases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clase = await _context.Clases.FindAsync(id);
            if (clase != null)
            {
                _context.Clases.Remove(clase);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaseExists(int id)
        {
            return _context.Clases.Any(e => e.Idclase == id);
        }
    }
}
