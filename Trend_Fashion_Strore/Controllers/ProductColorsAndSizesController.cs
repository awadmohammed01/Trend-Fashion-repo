using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trend_Fashion_Strore.Data;
using Trend_Fashion_Strore.Models;

namespace Trend_Fashion_Strore.Controllers
{
    public class ProductColorsAndSizesController : Controller
    {
        private readonly AppDbContext _context;

        public ProductColorsAndSizesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProductColorsAndSizes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProductColorsAndSizes.Include(p => p.Color).Include(p => p.Product).Include(p => p.Size);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProductColorsAndSizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productColorsAndSize = await _context.ProductColorsAndSizes
                .Include(p => p.Color)
                .Include(p => p.Product)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.ProductColorsAndSizeId == id);
            if (productColorsAndSize == null)
            {
                return NotFound();
            }

            return View(productColorsAndSize);
        }

        // GET: ProductColorsAndSizes/Create
        public IActionResult Create()
        {
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Categorize");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeName");
            return View();
        }

        // POST: ProductColorsAndSizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductColorsAndSizeId,ProductId,ColorId,SizeId,Quantity")] ProductColorsAndSize productColorsAndSize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productColorsAndSize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorId", productColorsAndSize.ColorId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Categorize", productColorsAndSize.ProductId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeName", productColorsAndSize.SizeId);
            return View(productColorsAndSize);
        }

        // GET: ProductColorsAndSizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productColorsAndSize = await _context.ProductColorsAndSizes.FindAsync(id);
            if (productColorsAndSize == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorId", productColorsAndSize.ColorId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Categorize", productColorsAndSize.ProductId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeName", productColorsAndSize.SizeId);
            return View(productColorsAndSize);
        }

        // POST: ProductColorsAndSizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductColorsAndSizeId,ProductId,ColorId,SizeId,Quantity")] ProductColorsAndSize productColorsAndSize)
        {
            if (id != productColorsAndSize.ProductColorsAndSizeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productColorsAndSize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductColorsAndSizeExists(productColorsAndSize.ProductColorsAndSizeId))
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
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorId", productColorsAndSize.ColorId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Categorize", productColorsAndSize.ProductId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeName", productColorsAndSize.SizeId);
            return View(productColorsAndSize);
        }

        // GET: ProductColorsAndSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productColorsAndSize = await _context.ProductColorsAndSizes
                .Include(p => p.Color)
                .Include(p => p.Product)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.ProductColorsAndSizeId == id);
            if (productColorsAndSize == null)
            {
                return NotFound();
            }

            return View(productColorsAndSize);
        }

        // POST: ProductColorsAndSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productColorsAndSize = await _context.ProductColorsAndSizes.FindAsync(id);
            if (productColorsAndSize != null)
            {
                _context.ProductColorsAndSizes.Remove(productColorsAndSize);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductColorsAndSizeExists(int id)
        {
            return _context.ProductColorsAndSizes.Any(e => e.ProductColorsAndSizeId == id);
        }
    }
}
