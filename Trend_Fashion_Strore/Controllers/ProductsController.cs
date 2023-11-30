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
            var TempAccount=_context.TempAccountInfos.Find(1);

            if(TempAccount !=null  && TempAccount.AccountId==1)
            {
                ModelState.AddModelError("not sing","not sing");

                return RedirectToAction("Login", "Accounts");
            }

            if(ModelState.IsValid)
            {
                IEnumerable<Product> product = _context.Products.Where(x => x.Categorize == categorize);


                IEnumerable<ProductImage> productImage = _context.ProductImages;
                ViewData["P"] = productImage;

                return View(product);
            }

            return RedirectToAction("Index", "Home");

        }




        public IActionResult ProductDetails(int ProductId)
        {
            //TempAccount code -------------------------

            var tempCustomer =  _context.TempAccountInfos.Find(1);

            if (tempCustomer != null && tempCustomer.AccountId != 1)
            {
                var custom = _context.Accounts.Find(tempCustomer.AccountId);
                ViewBag.CustomerName = custom?.Name;

                var prodnum = _context.Sales.Where(x => x.BasektId == tempCustomer.BasketId).Count();
                ViewBag.prodnum = prodnum;
            }


            //--------------------------------------



            IEnumerable<ProductImage> images = _context.ProductImages.Where(x=>x.ProductId==ProductId).ToList();
            ViewData["Images"] = images;

            var sizeColor = _context.ProductColorsAndSizes.Include(x=>x.Size).Include(x=>x.Color).Where(x => x.ProductId == ProductId).Distinct().ToList();

           
           
            ViewBag.sizeColor = sizeColor;




            //if(sizeColor!= null)
            //{
                


            //    var SizeList = new SelectList(sizeColor, "ProductColor", "ProductColor");
            //}
            



            return View();
        }










    }
}
