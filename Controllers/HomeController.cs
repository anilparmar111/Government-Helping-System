using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Government_Helping_System.Models;
using Microsoft.AspNetCore.Http;
using Government_Helping_System.Models.ViewsModel;

namespace Government_Helping_System.Controllers
{
    //[SessionAuthorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            //return RedirectToAction("Homepage", "Home");
            if (HttpContext.Session.Get("newuid") != null)
            {
                string uid = HttpContext.Session.GetString("newuid");
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("uid",uid);
                return View();
            }
            if (HttpContext.Session.Get("uid")!=null)
            {
                string uid = HttpContext.Session.GetString("uid");
                if(uid[0]=='C')
                {
                    return RedirectToAction("Homepage", "Citizen");
                }
                else if(uid[0]=='E')
                {
                    return RedirectToAction("Homepage", "Employees");
                }
                else
                {
                    return View();
                }
            }
            if(HttpContext.Session.Get("nfound")!=null)
            {
                HttpContext.Session.Clear();
                ViewBag.state = "false";
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("uid");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Validate(LoginViews loginView)
        {

            if(ModelState.IsValid)
            {
                if(loginView.uid[0]=='C')
                {
                    var citizen = _context.Citizens.FirstOrDefault(czn => czn.Id == loginView.uid && czn.Password == loginView.pswd);
                    if (citizen == null)
                    {
                        HttpContext.Session.SetString("nfound","true");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        HttpContext.Session.SetString("uid", loginView.uid);
                        return RedirectToAction("Homepage", "Citizen");
                    }
                }
                else if(loginView.uid[0]=='E')
                {
                    var employee = _context.employees.FirstOrDefault(emp => emp.Id == loginView.uid && emp.Password == loginView.pswd);
                    if (employee == null)
                    {
                        HttpContext.Session.SetString("nfound", "true");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        HttpContext.Session.SetString("uid", loginView.uid);
                        return RedirectToAction("Homepage", "Employees");
                    }
                }
                else
                {
                    HttpContext.Session.SetString("nfound", "true");
                    return RedirectToAction("Index", "Home");
                }    
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
