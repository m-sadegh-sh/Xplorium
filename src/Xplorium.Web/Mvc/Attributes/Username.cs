namespace Xplorium.Web.Mvc {
    using System.Web.Mvc;

    public class Username : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            const string userNameKey = "userName";
            if (filterContext.ActionParameters.ContainsKey(userNameKey))
                filterContext.ActionParameters[userNameKey] = (filterContext.HttpContext.User.Identity.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous";
        }
    }
}