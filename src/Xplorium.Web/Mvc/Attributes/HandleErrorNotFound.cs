namespace Xplorium.Web.Mvc {
    using System.Reflection;
    using System.Web.Mvc;

    using Xplorium.Core;

    public class HandleErrorNotFound : ActionFilterAttribute {
        private const string defaultViewName = "Not-Found";

        private string _viewName;

        public string ViewName {
            get { return !string.IsNullOrWhiteSpace(_viewName) ? _viewName : defaultViewName; }
            set { _viewName = value; }
        }

        public string MasterName { get; set; }

        public HandleErrorNotFound() : this(string.Empty) {}

        public HandleErrorNotFound(string viewName) : this(string.Empty, viewName) {}

        public HandleErrorNotFound(string masterName, string viewName) {
            MasterName = masterName;
            ViewName = viewName;
        }

        public void OnException(ExceptionContext filterContext) {
            var controller = filterContext.Controller as Controller;
            if (!CheckArgument.IsNull(() => controller) || filterContext.ExceptionHandled)
                return;

            var exception = filterContext.Exception;
            if (!CheckArgument.IsNull(() => exception))
                return;

            if (exception is TargetInvocationException)
                exception = exception.InnerException;

            if (!(exception is NotFoundException))
                return;

            filterContext.Result = new ViewResult {
                TempData = controller.TempData,
                ViewData = controller.ViewData,
                ViewName = ViewName
            };

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 404;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}