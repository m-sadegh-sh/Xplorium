namespace Xplorium.Web.Routing {
    using System;
    using System.Web;
    using System.Web.Routing;

    public class GuidRouteConstraint : IRouteConstraint {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection) {
            try {
                var parameter = values[parameterName].ToString();
                Guid temp;
                return (Guid.TryParse(parameter, out temp));
            } catch {
                return false;
            }
        }
    }
}