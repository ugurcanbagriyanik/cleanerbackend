using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using SharedLibrary.Models;
using SharedLibrary.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class LoginRequiredAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.Items["User"];
        if (user == null)
        {
            var response = new TDResponse();
            response.SetError("Unauthorized");
            // not logged in
            context.Result = new JsonResult(response) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class OnlyAdminAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var response = new TDResponse();
        try
        {
            PlayerDTO? user = (PlayerDTO?)context.HttpContext.Items["User"];
            if (user == null)
            {
                response.SetError("Unauthorized");
                context.Result = new JsonResult(response) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else if (!Environment.GetEnvironmentVariable("Admins")?.Split(',').Contains(user.Id.ToString()) ?? false)
            {
                response.SetError("ONLY_ADMIN");
                context.Result = new JsonResult(response) { StatusCode = StatusCodes.Status423Locked };
            }
        }
        catch (Exception e)
        {
            response.SetError("Unauthorized ERROR");
            context.Result = new JsonResult(response) { StatusCode = StatusCodes.Status423Locked };
        }
    }
}