using Microsoft.AspNetCore.Http;

namespace Pages.Tools.Http
{
    public static class UrlHelper
    {
        public static bool IsAdmin(HttpContext Context)
        {
            return Context.Request.Path.HasValue && Context.Request.Path.Value.ToLower().Contains("admin");
        }
    }
}
