namespace Xplorium.Web.Mvc {
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class PathAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            var viewData = filterContext.Controller.ViewData;

            var routeData = filterContext.RouteData;

            var currentArea = routeData.DataTokens["Area"] as string ?? string.Empty;
            var currentController = routeData.Values["Controller"] as string ?? string.Empty;
            var currentAction = routeData.Values["Action"] as string ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(currentArea))
                viewData.Add(new KeyValuePair<string, object>("CurrentArea", currentArea.ToLowerInvariant()));

            if (!string.IsNullOrWhiteSpace(currentController))
                viewData.Add(new KeyValuePair<string, object>("CurrentController", currentController.ToLowerInvariant()));

            if (!string.IsNullOrWhiteSpace(currentAction))
                viewData.Add(new KeyValuePair<string, object>("CurrentAction", currentAction.ToLowerInvariant()));
        }
    }
}