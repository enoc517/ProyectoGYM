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
    public class PedidosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pedidos.Include(p => p.IdempleadoNavigation).Include(p => p.IdprovedoresNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.IdempleadoNavigation)
                .Include(p => p.IdprovedoresNavigation)
                .FirstOrDefaultAsync(m => m.Idpedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["Idempleado"] = new SelectList(_context.Empleados, "Idempleado", "Apellidos");
            ViewData["Idprovedores"] = new SelectList(_context.Proveedores, "Idprovedores", "CorreoElectronico");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idpedido,Idempleado,Idprovedores,Estado,FechaCompra,FechaRecibido,Cantidad")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idempleado"] = new SelectList(_context.Empleados, "Idempleado", "Apellidos", pedido.Idempleado);
            ViewData["Idprovedores"] = new SelectList(_context.Proveedores, "Idprovedores", "CorreoElectronico", pedido.Idprovedores);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["Idempleado"] = new SelectList(_context.Empleados, "Idempleado", "Apellidos", pedido.Idempleado);
            ViewData["Idprovedores"] = new SelectList(_context.Proveedores, "Idprovedores", "CorreoElectronico", pedido.Idprovedores);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idpedido,Idempleado,Idprovedores,Estado,FechaCompra,FechaRecibido,Cantidad")] Pedido pedido)
        {
            if (id != pedido.Idpedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Idpedido))
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
            ViewData["Idempleado"] = new SelectList(_context.Empleados, "Idempleado", "Apellidos", pedido.Idempleado);
            ViewData["Idprovedores"] = new SelectList(_context.Proveedores, "Idprovedores", "CorreoElectronico", pedido.Idprovedores);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.IdempleadoNavigation)
                .Include(p => p.IdprovedoresNavigation)
                .FirstOrDefaultAsync(m => m.Idpedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Idpedido == id);
        }
    }
}
