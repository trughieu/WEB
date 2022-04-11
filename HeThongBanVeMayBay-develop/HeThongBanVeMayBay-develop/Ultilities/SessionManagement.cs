using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongBanVeMayBay.Ultilities
{
    public class SessionManagement
    {
    }
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext httpContext = HttpContext.Current;
            if(HttpContext.Current.Session["IsAdmin"] == null)
            {
                filterContext.Result = new RedirectResult("~/Authentication/Login");
                return;
            }    
            base.OnActionExecuting(filterContext);
        }
    }
}