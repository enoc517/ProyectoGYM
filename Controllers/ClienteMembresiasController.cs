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
    public class ClienteMembresiasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClienteMembresiasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClienteMembresias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClienteMembresia.Include(c => c.IdcategoraMembresiaNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClienteMembresias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteMembresium = await _context.ClienteMembresia
                .Include(c => c.IdcategoraMembresiaNavigation)
                .FirstOrDefaultAsync(m => m.IdclienteMembresia == id);
            if (clienteMembresium == null)
            {
                return NotFound();
            }

            return View(clienteMembresium);
        }

        // GET: ClienteMembresias/Create
        public IActionResult Create()
        {
            ViewData["IdcategoraMembresia"] = new SelectList(_context.CategoriaMembresia, "IdcategoriaMembresia", "Descripcion");
            return View();
        }

        // POST: ClienteMembresias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdclienteMembresia,IdcategoraMembresia,FechaProxRenovacion,FechaInicio,FechaFin")] ClienteMembresium clienteMembresium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteMembresium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdcategoraMembresia"] = new SelectList(_context.CategoriaMembresia, "IdcategoriaMembresia", "Descripcion", clienteMembresium.IdcategoraMembresia);
            return View(clienteMembresium);
        }

        // GET: ClienteMembresias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteMembresium = await _context.ClienteMembresia.FindAsync(id);
            if (clienteMembresium == null)
            {
                return NotFound();
            }
            ViewData["IdcategoraMembresia"] = new SelectList(_context.CategoriaMembresia, "IdcategoriaMembresia", "Descripcion", clienteMembresium.IdcategoraMembresia);
            return View(clienteMembresium);
        }

        // POST: ClienteMembresias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdclienteMembresia,IdcategoraMembresia,FechaProxRenovacion,FechaInicio,FechaFin")] ClienteMembresium clienteMembresium)
        {
            if (id != clienteMembresium.IdclienteMembresia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteMembresium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteMembresiumExists(clienteMembresium.IdclienteMembresia))
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
            ViewData["IdcategoraMembresia"] = new SelectList(_context.CategoriaMembresia, "IdcategoriaMembresia", "Descripcion", clienteMembresium.IdcategoraMembresia);
            return View(clienteMembresium);
        }

        // GET: ClienteMembresias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteMembresium = await _context.ClienteMembresia
                .Include(c => c.IdcategoraMembresiaNavigation)
                .FirstOrDefaultAsync(m => m.IdclienteMembresia == id);
            if (clienteMembresium == null)
            {
                return NotFound();
            }

            return View(clienteMembresium);
        }

        // POST: ClienteMembresias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteMembresium = await _context.ClienteMembresia.FindAsync(id);
            if (clienteMembresium != null)
            {
                _context.ClienteMembresia.Remove(clienteMembresium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteMembresiumExists(int id)
        {
            return _context.ClienteMembresia.Any(e => e.IdclienteMembresia == id);
        }
    }
}
