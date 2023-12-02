using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trend_Fashion_Strore.Data;

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

        [HttpGet("{basketId}")]
        public async Task<IActionResult> GetBasekts(int basketId)
        {
            var basekts = await _context.Basekts.Where(b => b.BasektId == basketId).ToListAsync();
            return Ok(basekts);
        }
    }
}
