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
            var sales = await _context.Sales
                .Include(s => s.Product).Include(s=>s.Color).Include(s=>s.Size) // تحميل البيانات المرتبطة بالمنتج
                .Where(s => s.BasektId == basketId)
                .ToListAsync();
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

            var basket = await _context.Basekts.FindAsync(sale.BasektId);

            if (basket == null)
            {
                return BadRequest("فشل الوصول للسلة");
            }

           

            // إضافة البيع إلى السلة
            _context.Sales.Add(sale);

            
            // تحديث سعر البيع في السلة
            basket.Total += sale.Quantity * sale.Price;
            _context.Entry(basket).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSale", new { id = sale.SaleId }, sale);
        }




        // PUT: api/Sales
        [HttpPut]
        public async Task<IActionResult> PutSales([FromBody] Sale sale)
        {
            var saleTb = await _context.Sales.FindAsync(sale.SaleId);

            if (saleTb == null)
            {
                return BadRequest();
            }

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
                .Where(s => s.ProductId == sale.ProductId && s.ColorId == sale.ColorId && s.SizeId == sale.SizeId && s.BasektId == sale.BasektId && s.SaleId != sale.SaleId)
                .SumAsync(s => s.Quantity);

            // طرح الكمية المعلقة من الكمية المتاحة
            var availableQuantity = productColorsAndSizes.Quantity - pendingSales;

            if (availableQuantity < sale.Quantity)
            {
                return BadRequest("الكمية غير متوفرة");
            }

            var basket = await _context.Basekts.FindAsync(sale.BasektId);

            if (basket == null)
            {
                return BadRequest("فشل الوصول للسلة");
            }

            var oldQuantity = saleTb.Quantity;

            saleTb.Quantity = sale.Quantity;

            // تحديث اجمالي السلة
            basket.Total -= oldQuantity * saleTb.Price;
            basket.Total += sale.Quantity * sale.Price;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }




        // وحدة التحكم لـ Sales
        [HttpDelete("{saleId}")]
        public async Task<IActionResult> DeleteSale(int saleId)
        {
            var sale = await _context.Sales.FindAsync(saleId);
            if (sale == null)
            {
                return NotFound();
            }

            var baskets = await _context.Basekts.FindAsync(sale.BasektId);

            if (baskets == null)
            {
                return BadRequest("فشل الوصول للسلة");
            }

           
            _context.Sales.Remove(sale);

            baskets.Total -= sale.Quantity * sale.Price;

            await _context.SaveChangesAsync();

            return NoContent();
        }





    }
}
