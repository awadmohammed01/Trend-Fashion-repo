using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System.Threading.Tasks;
using Trend_Fashion_Strore.Data;
using Trend_Fashion_Strore.Models;

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



        public IActionResult BasketDetails(int id)
        {
            //check tem account ------------------------------
            var TempAccount = _context.TempAccountInfos.Find(1);

            if (TempAccount != null && TempAccount.AccountId == 1)
            {
                ModelState.AddModelError("not sing", "not sing");

                return RedirectToAction("Login", "Accounts");
            }

            ViewBag.BasketId = id;
            //-------------------------------------------------


            if (TempAccount != null && TempAccount.AccountId != 1)
            {
                var custom = _context.Accounts.Find(TempAccount.AccountId);
                ViewBag.CustomerName = custom?.Name;

            }

            var basketId=_context.Basekts.Find(id); 

            if (basketId == null)
            {
                return NotFound();
            }
         

            ViewBag.BasketId=id;

            return View();
        }



        [HttpPost]
        public IActionResult Conf(Basekt basekt)
        {
            if (ModelState.IsValid)
            {
                var Bask = _context.Basekts.Find(basekt.BasektId);

                Bask.MoneyTransformNum = basekt.MoneyTransformNum;
                Bask.SaveStatus = true;

                _context.Basekts.Update(Bask);
               _context.SaveChanges();

                var temCustomer = _context.TempAccountInfos.Find(1);
                if (temCustomer != null)
                {
                    var Basket = new Basekt();
                    Basket.AccountId=Bask.AccountId;

                    _context.Basekts.Add(Basket);
                    _context.SaveChanges();

                    temCustomer.AccountId = Bask.AccountId;
                    temCustomer.BasketId = Basket.BasektId;
                    _context.Entry(temCustomer).State = EntityState.Modified;
                    _context.SaveChanges();
                }


            }

            return RedirectToAction("Index","Home");
        }


        [HttpPost]
        public IActionResult BaksetConf([Bind("id")] int id)
        {
            var Bask = _context.Basekts.Find(id);
            Bask.PaymentVerification = true;

            var sales = _context.Sales.Where(s => s.BasektId == id).ToList(); // Convert to list here
            foreach (var sale in sales)
            {
                var productColorSize = _context.ProductColorsAndSizes
                    .Where(p => p.ProductId == sale.ProductId && p.ColorId == sale.ColorId && p.SizeId == sale.SizeId)
                    .FirstOrDefault();
                if (productColorSize != null)
                {
                    if (productColorSize.Quantity >= sale.Quantity)
                    {
                        productColorSize.Quantity -= Convert.ToInt32(sale.Quantity);
                    }
                    else
                    {
                        Bask.PaymentVerification = false;
                        TempData["ConfEror"] = "الكميه غير كافيه في المستودع";
                        break;
                    }
                }
            }

            if (Bask.PaymentVerification == true)
            {
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    // Handle exception here
                }
            }

            return RedirectToAction("Index", "Baskets");
        }


    }
}
