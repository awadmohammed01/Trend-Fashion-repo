using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trend_Fashion_Strore.Data;
using Trend_Fashion_Strore.Models;

namespace Trend_Fashion_Strore.Controllers._api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // يتم حقن السياق الخاص بقاعدة البيانات هنا
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // هذه الوظيفة تعيد جميع المنتجات
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            // الحصول على المنتجات من قاعدة البيانات
            var products = await _context.Products.ToListAsync();

            return Ok(products);
        }

    

        // هذه الوظيفة تعيد المنتجات حسب الفئة
        [HttpGet("{Category}")]
        public async Task<IActionResult> GetProductsByCategory(string Category)
        {
            // الحصول على المنتجات حسب الفئة من قاعدة البيانات
            var products = await _context.Products.Where(x => x.Categorize == Category).ToListAsync();
            var productImages = await _context.ProductImages.Where(x => x.ImageName == "image0").ToListAsync();

            var result = products.Select(p => new
            {
                prodId=p.ProductId,
                Name = p.Name,
                Cost = p.Cost,
                profitMargin = p.ProfitMargin,
                ImagePath = productImages.FirstOrDefault(pi => pi.ProductId == p.ProductId)?.ImagePath
            });

            return Ok(result);
        }

        // GET: api/ProductImages/5
        [HttpGet("GetProductImages/{id}")]
        public async Task<IActionResult> GetProductImages(int id)
        {
            var productImage= await _context.ProductImages.Where(p => p.ProductId == id).ToListAsync();

            return Ok(productImage);
        }




        // هذه الوظيفة تنشئ منتجًا جديدًا
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            // إضافة المنتج إلى قاعدة البيانات
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // إرجاع المنتج الذي تم إنشاؤه كنتيجة JSON
            return Ok(product);
        }

        // هذه الوظيفة تحدث منتجًا موجودًا
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            // الحصول على المنتج من قاعدة البيانات
            var productToUpdate = await _context.Products.FindAsync(id);

            if (productToUpdate == null)
            {
                return NotFound("Product not found");
            }

            // تحديث خصائص المنتج
            productToUpdate.Name = product.Name;
            productToUpdate.ProfitMargin = product.ProfitMargin;
            productToUpdate.Cost = product.Cost;

            // حفظ التغييرات في قاعدة البيانات
            await _context.SaveChangesAsync();

            // إرجاع المنتج المحدث كنتيجة JSON
            return Ok(productToUpdate);
        }

        // هذه الوظيفة تحذف منتجًا موجودًا
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // الحصول على المنتج من قاعدة البيانات
            var productToDelete = await _context.Products.FindAsync(id);

            // إزالة المنتج من قاعدة البيانات
            _context.Products.Remove(productToDelete);
            await _context.SaveChangesAsync();

            // إرجاع رسالة نجاح كنتيجة JSON
            return Ok(new { message = "Product deleted successfully" });
        }
    }
}
