using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trend_Fashion_Strore.Data;
using Trend_Fashion_Strore.Models;

namespace Trend_Fashion_Strore.Controllers._api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // يتم حقن السياق الخاص بقاعدة البيانات هنا
        public BasketsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBasekts()
        {
            var basekts = await _context.Basekts
                .Include(b => b.Account).Where(x=>x.AccountId!=1) // تحميل البيانات المرتبطة بالحساب
                .ToListAsync();
            return Ok(basekts);
        }


        [HttpGet("{basketId}")]
        public async Task<IActionResult> GetBasekts(int basketId)
        {
            var basekts = await _context.Basekts
                .Include(b => b.Account) // تحميل البيانات المرتبطة بالحساب
                .Where(b => b.BasektId == basketId)
                .ToListAsync();
            return Ok(basekts);
        }


        // PUT: api/Baskets
        [HttpPut]
        public async Task<IActionResult> PutBasket([FromBody] Basekt basket)
        {
            var bask = _context.Basekts.Find(basket.BasektId);

            if (bask == null || bask.Total == null || bask.Total == 0)
            {
                return BadRequest();
            }

            bask.MoneyTransformNum = basket.MoneyTransformNum;
            bask.SaveStatus = true;
            _context.Entry(bask).State = EntityState.Modified;
            try
            {
                var temCustomer = _context.TempAccountInfos.Find(1);
                if (temCustomer != null)
                {
                    var Basket = new Basekt() { AccountId = bask.AccountId };

                    _context.Basekts.Add(Basket);
                    _context.SaveChanges();

                    temCustomer.AccountId = bask.AccountId;
                    temCustomer.BasketId = Basket.BasektId;
                    _context.Entry(temCustomer).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }



        //// PUT: api/ConfirmSale/5
        //[HttpPut("ConfirmSale/{id}")]
        //public async Task<IActionResult> ConfirmSale(int id)
        //{
        //    var basket = await _context.Basekts.FindAsync(id);

        //    if (basket == null)
        //    {
        //        return NotFound();
        //    }

        //    basket.PaymentVerification = true;

        //    var sales = _context.Sales.Where(s => s.BasektId == id);
        //    foreach (var sale in sales)
        //    {
        //        var productColorSize = await _context.ProductColorsAndSizes
        //            .Where(p => p.ProductId == sale.ProductId && p.ColorId == sale.ColorId && p.SizeId == sale.SizeId)
        //            .FirstOrDefaultAsync();
        //        if (productColorSize != null)
        //        {
        //            productColorSize.Quantity -=Convert.ToInt32( sale.Quantity);
        //        }
        //    }

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {

        //    }

        //    return NoContent();
        //}



        // DELETE: api/Baskets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(int id)
        {
            var basket = await _context.Basekts.FindAsync(id);
            if (basket == null)
            {
                return NotFound();
            }

            _context.Basekts.Remove(basket);
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}
