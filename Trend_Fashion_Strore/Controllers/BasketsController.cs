using Microsoft.AspNetCore.Mvc;
using Trend_Fashion_Strore.Data;

namespace Trend_Fashion_Strore.Controllers
{
    public class BasketsController : Controller
    {
        private readonly AppDbContext _context;

        // يتم حقن السياق الخاص بقاعدة البيانات هنا
        public BasketsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //check tem account ------------------------------
            var TempAccount = _context.TempAccountInfos.Find(1);

            if (TempAccount != null && TempAccount.AccountId == 1)
            {
                ModelState.AddModelError("not sing", "not sing");

                return RedirectToAction("Login", "Accounts");
            }

            //-------------------------------------------------

            
                if (TempAccount != null && TempAccount.AccountId != 1)
                {
                    var custom = _context.Accounts.Find(TempAccount.AccountId);
                    ViewBag.CustomerName = custom?.Name;

                }

            
                return View();
           

          


            
        }

        public async Task<IActionResult> TempBasket()
        {

            //check tem account ------------------------------
            var tempCustomer = _context.TempAccountInfos.Find(1);

            if (tempCustomer != null && tempCustomer.AccountId == 1)
            {
                ModelState.AddModelError("not sing", "not sing");

                return RedirectToAction("Login", "Accounts");
            }

            //-------------------------------------------------


            //TempAccount code send  custom name and saleCount to nav bar page -------------------------

           

            if (tempCustomer != null && tempCustomer.AccountId != 1)
            {
                var custom = _context.Accounts.Find(tempCustomer.AccountId);
                ViewBag.CustomerName = custom?.Name;

                var prodnum = _context.Sales.Where(x => x.BasektId == tempCustomer.BasketId).Count();
                ViewBag.prodnum = prodnum;
                ViewBag.BasketId = tempCustomer.BasketId;
            }


            //--------------------------------------



            if(tempCustomer == null)
            {
                return NotFound();
            }

            return View();
        }

    }
}
