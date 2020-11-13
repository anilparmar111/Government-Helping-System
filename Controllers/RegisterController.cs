using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Government_Helping_System.Models;
using Government_Helping_System.Models.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;

namespace Government_Helping_System.Controllers
{
    
    //[SessionAuthorize]
    [Route("Citizen/{action=Index}")]
    public class RegisterController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public RegisterController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env=env;
        }

        // GET: Register
        //[Route("")]
        //[Route("Index")]
        //[Route("/")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Citizens.ToListAsync());
        }


        public IActionResult HomePage()
        {
            
            if (HttpContext.Session.Get("uid") != null)
            {
                Citizen citizen = _context.Citizens.FirstOrDefault(czn => czn.Id == HttpContext.Session.GetString("uid"));
                //return "id is " + HttpContext.Session.GetString("uid");
                return View(citizen);
            }
            else
            {
                //return "not set session";
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: Register/Details/5
        //[Route("Details")]
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

        
        public IActionResult Query_Problem()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Query_Problem(QueriesView queriesView)
        {
            if(ModelState.IsValid)
            {
                
                if (queriesView.photos!=null)
                {
                    string folder = "Proofs/";
                    queriesView.uploadphoto = new List<UploadPhotoModel>();
                    foreach (var file in queriesView.photos)
                    {
                        var gallery = new UploadPhotoModel
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                        };
                        queriesView.uploadphoto.Add(gallery);
                    }
                }

                string filepath="";

                if(queriesView.textfile!=null)
                {
                    string folderPath = "Description/";
                    folderPath += Guid.NewGuid().ToString() + "_" + RandomFileName(6);
                    filepath = Path.Combine(_env.WebRootPath, folderPath);
                    filepath += ".txt";
                    System.IO.File.WriteAllText(filepath, queriesView.textfile);
                }
                Querie querie = new Querie();
                
                querie.Id = RandomString(15);
                querie.CitizenId = HttpContext.Session.GetString("uid");
                querie.status = "Req";
                querie.Area = queriesView.Area;
                querie.zipcode = queriesView.zipcode;
                querie.textfilepath = filepath;
                querie.title = queriesView.title;
                querie.Citizen = _context.Citizens.FirstOrDefault(czn => czn.Id == HttpContext.Session.GetString("uid"));
                querie.ProofPhotos = new List<PhotoModel>();
                foreach (var file in queriesView.uploadphoto)
                {
                    querie.ProofPhotos.Add(new PhotoModel() {
                        Name = file.Name,
                        URL = file.URL
                    });
                }

                await _context.queries.AddAsync(querie);
                await _context.SaveChangesAsync();

                return RedirectToAction("HomePage","Citizen");
            }
            else
            {
                return View();
            }
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_env.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
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
                newuid = "C" + newuid;
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
                ViewBag.uid = newuid;
                HttpContext.Session.SetString("newuid", newuid);
                return this.RedirectToAction("Index","Home");
                //return RedirectToAction(nameof(Index));
            }
            return View(citizen);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomFileName(int length)
        {
            const string chars = "abcdefghigklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
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
