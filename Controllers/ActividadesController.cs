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
    public class ActividadesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActividadesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Actividades
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Actividades.Include(a => a.IdsalaNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Actividades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actividade = await _context.Actividades
                .Include(a => a.IdsalaNavigation)
                .FirstOrDefaultAsync(m => m.Idactividades == id);
            if (actividade == null)
            {
                return NotFound();
            }

            return View(actividade);
        }

        // GET: Actividades/Create
        public IActionResult Create()
        {
            ViewData["Idsala"] = new SelectList(_context.Salas, "Idsala", "Idsala");
            return View();
        }

        // POST: Actividades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idactividades,Idsala,Descripcion")] Actividade actividade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actividade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idsala"] = new SelectList(_context.Salas, "Idsala", "Idsala", actividade.Idsala);
            return View(actividade);
        }

        // GET: Actividades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actividade = await _context.Actividades.FindAsync(id);
            if (actividade == null)
            {
                return NotFound();
            }
            ViewData["Idsala"] = new SelectList(_context.Salas, "Idsala", "Idsala", actividade.Idsala);
            return View(actividade);
        }

        // POST: Actividades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idactividades,Idsala,Descripcion")] Actividade actividade)
        {
            if (id != actividade.Idactividades)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actividade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActividadeExists(actividade.Idactividades))
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
            ViewData["Idsala"] = new SelectList(_context.Salas, "Idsala", "Idsala", actividade.Idsala);
            return View(actividade);
        }

        // GET: Actividades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actividade = await _context.Actividades
                .Include(a => a.IdsalaNavigation)
                .FirstOrDefaultAsync(m => m.Idactividades == id);
            if (actividade == null)
            {
                return NotFound();
            }

            return View(actividade);
        }

        // POST: Actividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actividade = await _context.Actividades.FindAsync(id);
            if (actividade != null)
            {
                _context.Actividades.Remove(actividade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActividadeExists(int id)
        {
            return _context.Actividades.Any(e => e.Idactividades == id);
        }
    }
}
