using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

public class AdminAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public bool AllowMultiple => false; // Implement the AllowMultiple property

    public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
    {
        // Check if the user is authenticated and isAdmin is true
        if (actionContext.RequestContext.Principal.Identity.IsAuthenticated)
        {
            var isAdminClaim = ((ClaimsPrincipal)actionContext.RequestContext.Principal).FindFirst("isAdmin");
            if (isAdminClaim != null && bool.TryParse(isAdminClaim.Value, out bool isAdminValue) && isAdminValue)
            {
                return await continuation();
            }
        }

        // Unauthorized access
        return actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized access");
    }
}
