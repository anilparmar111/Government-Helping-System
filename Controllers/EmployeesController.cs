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
using Newtonsoft.Json;

namespace Government_Helping_System.Controllers
{
    //[SessionAuthorize]
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Proceed(string Id)
        {
            Querie querie = _context.queries.FirstOrDefault(qry => qry.Id == Id);
            Employee employee = _context.employees.FirstOrDefault(emp=>emp.Id==HttpContext.Session.GetString("uid"));
            if (employee.post == "main")
            {
                querie.status = "Acc";
                querie.EmployeeId = HttpContext.Session.GetString("uid");
            }
            else
            {
                querie.status = "Inp";
                querie.Main_Emp_Id = HttpContext.Session.GetString("uid");
            }
            _context.SaveChanges();
            return RedirectToAction("HomePage","Employees");
        }

        public IActionResult ProblemInProcess()
        {
            string eid = HttpContext.Session.GetString("uid");
            Employee employee = _context.employees.FirstOrDefault(emp => emp.Id == eid);
            string zipcode = employee.ZipCode;
            IEnumerable<Querie> queries = null;
            queries = _context.queries.Where(que => que.zipcode == zipcode &&  que.status == "Inp").OrderBy(que => que.Query_Time);
            return View(queries);
        }

        public IActionResult Completed(string Id)
        {
            Querie querie = _context.queries.FirstOrDefault(qry => qry.Id == Id);
            querie.status = "Comp";
            _context.SaveChanges();
            return RedirectToAction("HomePage","Employees");
        }

        public IActionResult Reject(string Id)
        {
            Querie querie = _context.queries.FirstOrDefault(qry => qry.Id == Id);
            querie.status = "Rej";
            querie.EmployeeId = HttpContext.Session.GetString("uid");
            _context.SaveChanges();
            return RedirectToAction("HomePage", "Employees");
        }

        public IActionResult ProblemToDo()
        {
            string eid = HttpContext.Session.GetString("uid");
            Employee employee = _context.employees.FirstOrDefault(emp=>emp.Id==eid);
            string zipcode = employee.ZipCode;
            IEnumerable<Querie> queries=null;
            if (employee.post == "main")
            {
                queries = _context.queries.Where(que => que.zipcode == zipcode && que.status == "Req").OrderBy(que => que.Query_Time);
                if (queries == null)
                {
                    ViewBag.req = "no";
                }
            }
            else
            {
                queries = _context.queries.Where(que => que.zipcode == zipcode && que.status == "Acc").OrderBy(que => que.Query_Time);
                if (queries == null)
                {
                    ViewBag.req = "no";
                }
            }
            return View(queries);
        }

        public IActionResult ProblemDone()
        {
            string uid = HttpContext.Session.GetString("uid");
            IEnumerable<Querie> queries = _context.queries.Where(que =>(que.EmployeeId==uid || que.Main_Emp_Id==uid) && que.status=="Comp").OrderByDescending(que=>que.Query_Time);
            return View(queries);
        }

        public IActionResult Query_Details(string Id)
        {
            Querie querie = _context.queries.FirstOrDefault(qry => qry.Id == Id);
            QueryViewModel queryViewModel = new QueryViewModel();
            if (querie != null)
            {
                queryViewModel.Id = querie.Id;
                queryViewModel.Title = querie.title;
                queryViewModel.UploadTime = querie.Query_Time;
                queryViewModel.Description = System.IO.File.ReadAllText(querie.textfilepath);
                IEnumerable<PhotoModel> photoModels = _context.photoModels.Where(ph => ph.querieId == Id);
                if (photoModels == null)
                {
                    ViewBag.Error = "error";
                }
                else
                {
                    List<string> urls = new List<string>();
                    List<string> names = new List<string>();
                    foreach (var photo in photoModels)
                    {
                        urls.Add(photo.URL);
                        names.Add(photo.Name);
                    }
                    queryViewModel.URLS = urls;
                    queryViewModel.Names = names;
                }
                Employee employee = _context.employees.FirstOrDefault(emp=>emp.Id==HttpContext.Session.GetString("uid"));
                if(employee.post=="main")
                {
                    if(querie.status=="Inp" || querie.status=="Comp")
                    {
                        ViewBag.show = "none";
                    }
                    else
                    {
                        ViewBag.show = "all";
                    }
                }
                else
                {
                    if(querie.status=="Comp")
                    {
                        ViewBag.show = "none";
                    }
                    else if(querie.status=="Inp")
                    {
                        ViewBag.show = "Complete";
                    }
                    else if(querie.status=="Acc")
                    {
                        ViewBag.show = "proceed Request";
                    }
                }
                return View(queryViewModel);
            }
            ViewBag.Error = "error";
            return View();
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            if(HttpContext.Session.GetString("uid")==null)
            {
                return RedirectToAction("Create", "Employees");
            }
            else
            {
                return RedirectToAction("HomePage", "Employees");
            }
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public IActionResult HomePage()
        {

            if (HttpContext.Session.Get("uid") != null)
            {
                Employee employee = _context.employees.FirstOrDefault(emp => emp.Id == HttpContext.Session.GetString("uid"));
                //return "id is " + HttpContext.Session.GetString("uid");

                return View(employee);
            }
            else
            {
                //return "not set session";
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViews employeeViews)
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
                newuid = "E" + newuid;
                Employee employee = new Employee();
                employee.Id = newuid;
                employee.Name = employeeViews.Name;
                employee.Email = employeeViews.Email;
                employee.Password = employeeViews.Password;
                employee.Area = employeeViews.Area;
                employee.ZipCode = employeeViews.ZipCode;
                employee.post = employeeViews.post;
                _context.Add(employee);
                await _context.SaveChangesAsync();
                ViewBag.uid = newuid;
                HttpContext.Session.SetString("newuid", newuid);
                return this.RedirectToAction("Index", "Home");
            }
            return View(employeeViews);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Email,Password,ConfirmPassword,Area,ZipCode,post")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employee = await _context.employees.FindAsync(id);
            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(string id)
        {
            return _context.employees.Any(e => e.Id == id);
        }
    }
}
