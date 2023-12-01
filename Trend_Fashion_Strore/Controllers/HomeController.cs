using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Trend_Fashion_Strore.Data;
using Trend_Fashion_Strore.Models;

namespace Trend_Fashion_Strore.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //chick temp account code -------------------------

            var tempCustomer = await _context.TempAccountInfos.FindAsync(1);

            if (tempCustomer != null && tempCustomer.AccountId != 1)
            {
                var custom = _context.Accounts.Find(tempCustomer.AccountId);
                ViewBag.CustomerName = custom?.Name;

                var prodnum = _context.Sales.Where(x => x.BasektId == tempCustomer.BasketId).Count();
                ViewBag.prodnum = prodnum;
            }


            //--------------------------------------


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
