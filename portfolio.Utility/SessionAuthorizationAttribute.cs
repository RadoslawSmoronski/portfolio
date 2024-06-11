using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class SessionAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var session = context.HttpContext.Session;
        var isActiveSession = session.GetString("IsActiveSession");

        if (string.IsNullOrEmpty(isActiveSession) || isActiveSession != "true")
        {
            context.Result = new NotFoundResult();
        }
    }
}