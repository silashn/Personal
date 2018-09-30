using Microsoft.AspNetCore.Mvc.Rendering;

namespace Playground.Tools.HTML.URL
{
    public static class UrlManager
    {
        public static bool IsCurrent(ViewContext viewContext, string controller, string action)
        {
            return (viewContext.RouteData.Values["controller"].Equals(controller) && viewContext.RouteData.Values["action"].Equals(action));
        }
    }
}
