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
            return View();
        }

        public string Validate(LoginViews loginView)
        {

            //if(ModelState.IsValid)
            //{
            var citizen = _context.Citizens.FirstOrDefault(czn => czn.Id == loginView.uid
           && czn.Password == loginView.pswd
            );
            if (citizen == null)
            {
                return "Not Found";
            }
            else
            {

                HttpContext.Session.SetString("uid", loginView.uid);
                if (loginView.remember)
                {
                    var option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Append("uid", loginView.uid, option);
                }
                return "Ok Now You are in";

            }
            /*}
            else
            {
                return "not found  object";
            }*/


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
