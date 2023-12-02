using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trend_Fashion_Strore.Data;
using Trend_Fashion_Strore.Models;

namespace Trend_Fashion_Strore.Controllers._api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext _context; // استبدل هذا بالسياق الخاص بك

        public SalesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{basketId}")]
        public async Task<IActionResult> GetSales(int basketId)
        {
            var sales = await _context.Sales.Where(s => s.BasektId == basketId).ToListAsync();
            return Ok(sales);
        }

        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(Sale sale)
        {
            // التحقق من وجود كمية من المنتج
            var product = await _context.Products.FindAsync(sale.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            var productColorsAndSizes = await _context.ProductColorsAndSizes
                .Where(p => p.ProductId == sale.ProductId && p.ColorId == sale.ColorId && p.SizeId == sale.SizeId)
                .FirstOrDefaultAsync();

            if (productColorsAndSizes == null)
            {
                return BadRequest("الكمية غير متوفرة");
            }

            // جمع كميات عمليات البيع من نفس المنتج واللون والمقاس والسلة
            var pendingSales = await _context.Sales
                .Where(s => s.ProductId == sale.ProductId && s.ColorId == sale.ColorId && s.SizeId == sale.SizeId && s.BasektId == sale.BasektId)
                .SumAsync(s => s.Quantity);

            // طرح الكمية المعلقة من الكمية المتاحة
            var availableQuantity = productColorsAndSizes.Quantity - pendingSales;

            if (availableQuantity < sale.Quantity)
            {
                return BadRequest("الكمية غير متوفرة");
            }

            //// خصم الكمية من ProductColorsAndSize
            //productColorsAndSizes.Quantity -= sale.Quantity;
            //_context.Entry(productColorsAndSizes).State = EntityState.Modified;

            // إضافة البيع إلى السلة
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSale", new { id = sale.SaleId }, sale);
        }



    }
}
