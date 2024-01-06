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
    public class GeneracionPagosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GeneracionPagosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GeneracionPagos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GeneracionPagos.Include(g => g.IdjornadasNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GeneracionPagos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generacionPago = await _context.GeneracionPagos
                .Include(g => g.IdjornadasNavigation)
                .FirstOrDefaultAsync(m => m.IdgeneracionPago == id);
            if (generacionPago == null)
            {
                return NotFound();
            }

            return View(generacionPago);
        }

        // GET: GeneracionPagos/Create
        public IActionResult Create()
        {
            ViewData["Idjornadas"] = new SelectList(_context.Jornadas, "Idjornadas", "Idjornadas");
            return View();
        }

        // POST: GeneracionPagos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdgeneracionPago,Idjornadas,DescripcionPago,TipoPago,SalarioBruto,EstadoDelPago,Feriados,HorasExtras")] GeneracionPago generacionPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(generacionPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idjornadas"] = new SelectList(_context.Jornadas, "Idjornadas", "Idjornadas", generacionPago.Idjornadas);
            return View(generacionPago);
        }

        // GET: GeneracionPagos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generacionPago = await _context.GeneracionPagos.FindAsync(id);
            if (generacionPago == null)
            {
                return NotFound();
            }
            ViewData["Idjornadas"] = new SelectList(_context.Jornadas, "Idjornadas", "Idjornadas", generacionPago.Idjornadas);
            return View(generacionPago);
        }

        // POST: GeneracionPagos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdgeneracionPago,Idjornadas,DescripcionPago,TipoPago,SalarioBruto,EstadoDelPago,Feriados,HorasExtras")] GeneracionPago generacionPago)
        {
            if (id != generacionPago.IdgeneracionPago)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(generacionPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneracionPagoExists(generacionPago.IdgeneracionPago))
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
            ViewData["Idjornadas"] = new SelectList(_context.Jornadas, "Idjornadas", "Idjornadas", generacionPago.Idjornadas);
            return View(generacionPago);
        }

        // GET: GeneracionPagos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generacionPago = await _context.GeneracionPagos
                .Include(g => g.IdjornadasNavigation)
                .FirstOrDefaultAsync(m => m.IdgeneracionPago == id);
            if (generacionPago == null)
            {
                return NotFound();
            }

            return View(generacionPago);
        }

        // POST: GeneracionPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var generacionPago = await _context.GeneracionPagos.FindAsync(id);
            if (generacionPago != null)
            {
                _context.GeneracionPagos.Remove(generacionPago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneracionPagoExists(int id)
        {
            return _context.GeneracionPagos.Any(e => e.IdgeneracionPago == id);
        }
    }
}
