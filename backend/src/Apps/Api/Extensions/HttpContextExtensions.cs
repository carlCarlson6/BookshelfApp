using System.Security.Claims;

namespace Api.Extensions;

public static class HttpContextExtensions
{
    public static string GetIdentifiedUserId(this HttpContext httpContext) => 
        httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
} 