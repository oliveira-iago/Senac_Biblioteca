using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("user")))
            {
                filterContext.HttpContext.Response.Redirect("/Home/Login");
            }
        }
    }
}