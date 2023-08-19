using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

public class AdminAuthorizationAttribute : AuthorizeAttribute
{
    protected override bool IsAuthorized(HttpActionContext actionContext)
    {
        var authCookie = HttpContext.Current.Request.Cookies["AuthCookie"];

        if (authCookie != null && bool.TryParse(authCookie["IsAdmin"], out bool isAdmin))
        {
            return isAdmin;
        }

        return false;
    }
}
