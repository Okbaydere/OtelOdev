using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtelUI.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            
            // Tüm response'lar için UTF-8 encoding'i ayarla
            filterContext.HttpContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            filterContext.HttpContext.Response.ContentType = "text/html; charset=utf-8";
        }
    }
}
