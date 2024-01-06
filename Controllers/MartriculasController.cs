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
    public class MartriculasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MartriculasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Martriculas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Martriculas.Include(m => m.IdclienteNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Martriculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var martricula = await _context.Martriculas
                .Include(m => m.IdclienteNavigation)
                .FirstOrDefaultAsync(m => m.Idmatricula == id);
            if (martricula == null)
            {
                return NotFound();
            }

            return View(martricula);
        }

        // GET: Martriculas/Create
        public IActionResult Create()
        {
            ViewData["Idcliente"] = new SelectList(_context.Clientes, "Idcliente", "Apellidos");
            return View();
        }

        // POST: Martriculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idmatricula,Idclase,Idcliente,Estado")] Martricula martricula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(martricula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcliente"] = new SelectList(_context.Clientes, "Idcliente", "Apellidos", martricula.Idcliente);
            return View(martricula);
        }

        // GET: Martriculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var martricula = await _context.Martriculas.FindAsync(id);
            if (martricula == null)
            {
                return NotFound();
            }
            ViewData["Idcliente"] = new SelectList(_context.Clientes, "Idcliente", "Apellidos", martricula.Idcliente);
            return View(martricula);
        }

        // POST: Martriculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idmatricula,Idclase,Idcliente,Estado")] Martricula martricula)
        {
            if (id != martricula.Idmatricula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(martricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MartriculaExists(martricula.Idmatricula))
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
            ViewData["Idcliente"] = new SelectList(_context.Clientes, "Idcliente", "Apellidos", martricula.Idcliente);
            return View(martricula);
        }

        // GET: Martriculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var martricula = await _context.Martriculas
                .Include(m => m.IdclienteNavigation)
                .FirstOrDefaultAsync(m => m.Idmatricula == id);
            if (martricula == null)
            {
                return NotFound();
            }

            return View(martricula);
        }

        // POST: Martriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var martricula = await _context.Martriculas.FindAsync(id);
            if (martricula != null)
            {
                _context.Martriculas.Remove(martricula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MartriculaExists(int id)
        {
            return _context.Martriculas.Any(e => e.Idmatricula == id);
        }
    }
}
