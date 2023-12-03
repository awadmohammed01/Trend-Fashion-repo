using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trend_Fashion_Strore.Data;
using Trend_Fashion_Strore.Models;

namespace Trend_Fashion_Strore.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string categorize)
        {
            //check tem account ------------------------------
            var TempAccount=_context.TempAccountInfos.Find(1);

            if(TempAccount !=null  && TempAccount.AccountId==1)
            {
                ModelState.AddModelError("not sing","not sing");

                return RedirectToAction("Login", "Accounts");
            }

            //-------------------------------------------------

            if(ModelState.IsValid)
            {
                if (TempAccount != null && TempAccount.AccountId != 1)
                {
                    var custom = _context.Accounts.Find(TempAccount.AccountId);
                    ViewBag.CustomerName = custom?.Name;

                    var prodnum = _context.Sales.Where(x => x.BasektId == TempAccount.BasketId).Count();
                    ViewBag.prodnum = prodnum;
                }

                ViewBag.Categorey = categorize;
                return View();
            }

            return RedirectToAction("Index", "Home");

        }




        public IActionResult ProductDetails(int id)
        {
            //TempAccount code -------------------------

            var tempCustomer =  _context.TempAccountInfos.Find(1);

            if (tempCustomer != null && tempCustomer.AccountId != 1)
            {
                var custom = _context.Accounts.Find(tempCustomer.AccountId);
                ViewBag.CustomerName = custom?.Name;

                var prodnum = _context.Sales.Where(x => x.BasektId == tempCustomer.BasketId).Count();
                ViewBag.prodnum = prodnum;
                ViewBag.BasketId = tempCustomer.BasketId;
            }


            //--------------------------------------
            //prodId-------------------

            ViewBag.ProductId = id;




            // Get the product details from the database
            var product =  _context.Products.Find(id);
           

            // Get the product colors and sizes from the database
            var productColorsAndSizes =  _context.ProductColorsAndSizes
                .Include(p => p.Color)
                .Include(p => p.Size)
                .Where(p => p.ProductId == id)
                .ToList();

            // Create select lists for the colors and sizes
            ViewBag.ColorSelect = new SelectList(productColorsAndSizes.Select(p => p.Color).Distinct(), "ColorId", "ColorName");
            ViewBag.SizeSelect = new SelectList(productColorsAndSizes.Select(p => p.Size).Distinct(), "SizeId", "SizeName");
            ViewBag.Product = product;





            return View();
        }










    }
}
