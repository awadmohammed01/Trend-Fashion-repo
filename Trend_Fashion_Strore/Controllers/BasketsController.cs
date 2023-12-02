using Microsoft.AspNetCore.Mvc;

namespace Trend_Fashion_Strore.Controllers
{
    public class BasketsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TempBasket(int id)
        {
            ViewBag.BasketId = id;
            return View();
        }

    }
}
