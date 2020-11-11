using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Government_Helping_System.Models;
using Government_Helping_System.Models.ViewsModel;

namespace Government_Helping_System.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AppDbContext _context;

        public RegisterController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Register
        public async Task<IActionResult> Index()
        {
            return View(await _context.Citizens.ToListAsync());
        }

        // GET: Register/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }

        // GET: Register/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Register/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CitizenViews citizen)
        {
            if (ModelState.IsValid)
            {
                string newuid;
                while (true)
                {
                    newuid = RandomString(11);
                    var find = await _context.Citizens.FirstOrDefaultAsync(m => m.Id == newuid);
                    if (find == null)
                    {
                        break;
                    }
                }
                newuid = "A" + newuid;
                Citizen newcitizen = new Citizen();
                newcitizen.Id = newuid;
                newcitizen.Name = citizen.Name;
                newcitizen.PhoneNumber = citizen.PhoneNumber;
                newcitizen.Email = citizen.Email;
                newcitizen.Password = citizen.Password;
                newcitizen.Date_of_Birth = citizen.Date_of_Birth;
                newcitizen.City = citizen.City;
                newcitizen.Area = citizen.Area;
                newcitizen.ZipCode = citizen.ZipCode;
                _context.Add(newcitizen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(citizen);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "aAbc96665656de1f2g3h4i5j65k7l8m9n065446oBC545189685DEFGHIJKLMNOP957654651454689797654216575415QRSTUVWXYZpqrstuvwxyz!#$%{}?&0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        // GET: Register/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizens.FindAsync(id);
            if (citizen == null)
            {
                return NotFound();
            }
            return View(citizen);
        }

        // POST: Register/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,PhoneNumber,Email,Password,Date_of_Birth,City,Area,ZipCode")] Citizen citizen)
        {
            if (id != citizen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citizen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitizenExists(citizen.Id))
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
            return View(citizen);
        }

        // GET: Register/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }

        // POST: Register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var citizen = await _context.Citizens.FindAsync(id);
            _context.Citizens.Remove(citizen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitizenExists(string id)
        {
            return _context.Citizens.Any(e => e.Id == id);
        }
    }
}
