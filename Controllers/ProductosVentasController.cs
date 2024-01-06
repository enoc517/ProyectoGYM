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
    public class ProductosVentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosVentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductosVentas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductosVenta.Include(p => p.IdcategoriaProductoNavigation).Include(p => p.IdprovedoresNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductosVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productosVentum = await _context.ProductosVenta
                .Include(p => p.IdcategoriaProductoNavigation)
                .Include(p => p.IdprovedoresNavigation)
                .FirstOrDefaultAsync(m => m.Idproductos == id);
            if (productosVentum == null)
            {
                return NotFound();
            }

            return View(productosVentum);
        }

        // GET: ProductosVentas/Create
        public IActionResult Create()
        {
            ViewData["IdcategoriaProducto"] = new SelectList(_context.CategoriaProductos, "IdcategoriaProducto", "Descripcion");
            ViewData["Idprovedores"] = new SelectList(_context.Proveedores, "Idprovedores", "CorreoElectronico");
            return View();
        }

        // POST: ProductosVentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idproductos,Idprovedores,Nombre,PrecioUnitario,Cantidad,IdcategoriaProducto,FechaCaducidad")] ProductosVentum productosVentum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productosVentum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdcategoriaProducto"] = new SelectList(_context.CategoriaProductos, "IdcategoriaProducto", "Descripcion", productosVentum.IdcategoriaProducto);
            ViewData["Idprovedores"] = new SelectList(_context.Proveedores, "Idprovedores", "CorreoElectronico", productosVentum.Idprovedores);
            return View(productosVentum);
        }

        // GET: ProductosVentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productosVentum = await _context.ProductosVenta.FindAsync(id);
            if (productosVentum == null)
            {
                return NotFound();
            }
            ViewData["IdcategoriaProducto"] = new SelectList(_context.CategoriaProductos, "IdcategoriaProducto", "Descripcion", productosVentum.IdcategoriaProducto);
            ViewData["Idprovedores"] = new SelectList(_context.Proveedores, "Idprovedores", "CorreoElectronico", productosVentum.Idprovedores);
            return View(productosVentum);
        }

        // POST: ProductosVentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idproductos,Idprovedores,Nombre,PrecioUnitario,Cantidad,IdcategoriaProducto,FechaCaducidad")] ProductosVentum productosVentum)
        {
            if (id != productosVentum.Idproductos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productosVentum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosVentumExists(productosVentum.Idproductos))
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
            ViewData["IdcategoriaProducto"] = new SelectList(_context.CategoriaProductos, "IdcategoriaProducto", "Descripcion", productosVentum.IdcategoriaProducto);
            ViewData["Idprovedores"] = new SelectList(_context.Proveedores, "Idprovedores", "CorreoElectronico", productosVentum.Idprovedores);
            return View(productosVentum);
        }

        // GET: ProductosVentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productosVentum = await _context.ProductosVenta
                .Include(p => p.IdcategoriaProductoNavigation)
                .Include(p => p.IdprovedoresNavigation)
                .FirstOrDefaultAsync(m => m.Idproductos == id);
            if (productosVentum == null)
            {
                return NotFound();
            }

            return View(productosVentum);
        }

        // POST: ProductosVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productosVentum = await _context.ProductosVenta.FindAsync(id);
            if (productosVentum != null)
            {
                _context.ProductosVenta.Remove(productosVentum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosVentumExists(int id)
        {
            return _context.ProductosVenta.Any(e => e.Idproductos == id);
        }
    }
}
