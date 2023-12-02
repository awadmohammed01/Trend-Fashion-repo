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

        public BasketsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Baskets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Basekt>> GetBasket(int id)
        {
            var basket = await _context.Basekts.FindAsync(id);

            if (basket == null)
            {
                return NotFound();
            }

            return basket;
        }

        // GET: api/Baskets/5/Sales
        [HttpGet("{id}/Sales")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSalesForBasket(int id)
        {
            return await _context.Sales.Where(s => s.BasektId == id).ToListAsync();
        }

        // PUT: api/Baskets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBasket(int id, Basekt basket)
        {
            if (id != basket.BasektId)
            {
                return BadRequest();
            }

            _context.Entry(basket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BasketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool BasketExists(int id)
        {
            return _context.Basekts.Any(e => e.BasektId == id);
        }
    }

}
