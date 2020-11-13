using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;

namespace Government_Helping_System.Controllers
{
    public class SessionAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected  bool AuthorizeCore(HttpContext httpContext)
        {
            if(httpContext.Session.Get("uid")!=null)
            {
                return false;
            }
            else
            {
                return (true);
            }
        }

        protected override  void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Home/Index");
        }
    }
}
