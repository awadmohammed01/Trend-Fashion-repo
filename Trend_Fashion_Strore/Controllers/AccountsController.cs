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
    public class AccountsController : Controller
    {
        private readonly AppDbContext _context;

        public AccountsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Accounts.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Account account)
        {
           
            account.CreatedAt = DateTime.Now;
            account.UpdatedAt = DateTime.Now;
            account.Deleted = false;

            if (ModelState.IsValid)
            {
                await _context.Accounts.AddAsync(account);

                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Accounts");

            }
            TempData["faled"] = "فشل اضافه العميل";
            return RedirectToAction("Index", "Home");
        }


        // GET: Customers/login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Name")] string Name, [Bind("Name")] string Password)
        {
            var Customer = await _context.Accounts.FirstOrDefaultAsync(x => x.Name == Name);
            var CustomerPass = await _context.Accounts.FirstOrDefaultAsync(x => x.Name == Name && x.Password == Password);

            string erm = "error login";

            if (Customer == null)
            {
                ModelState.AddModelError("Name", "the name is not found");

                erm = "the name is not found";

            }

            else if (Customer != null && CustomerPass == null)
            {
                ModelState.AddModelError("Password", "the password is not Corect");

                erm = "the password is not Corect";
            }

            if (ModelState.IsValid)
            {

                try
                {
                    var temCustomer = _context.TempAccountInfos.Where(x => x.TempId == 1).FirstOrDefault();

                    // الشرط مجرد لتسريع قراءه الكود
                    if (temCustomer != null && Customer != null)
                    {
                        var existBasket = _context.Basekts.Where(x => x.AccountId == Customer.AccountId && x.SaveStatus != true).FirstOrDefault();

                        if (existBasket != null)
                        {
                            temCustomer.AccountId = Customer.AccountId;
                            //هنا بمعنى سله ال billid
                            temCustomer.BasketId = existBasket.BasektId;
                            _context.Update(temCustomer);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var Basket = new Basekt() { AccountId = Customer.AccountId };

                            _context.Basekts.Add(Basket);
                            _context.SaveChanges();

                            temCustomer.AccountId = Customer.AccountId;
                            temCustomer.BasketId = Basket.BasektId;
                            _context.Update(temCustomer);
                            await _context.SaveChangesAsync();
                        }











                    }


                    return RedirectToAction("Index", "Home");

                }
                catch 
                {
                   
                }



            }
            else
            {
                TempData["errorlogin"] = erm;
            }



            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginOut()
        {
            var tempCustomer = await _context.TempAccountInfos.FindAsync(1);

            if (tempCustomer != null)
            {
                tempCustomer.AccountId = 1;
                tempCustomer.BasketId= 1;
                _context.Update(tempCustomer);
                _context.SaveChanges();
            }




            return RedirectToAction("Index", "Home");
        }


        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,Name,Email,Address,Note,Password,Deleted,CreatedAt,UpdatedAt")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
