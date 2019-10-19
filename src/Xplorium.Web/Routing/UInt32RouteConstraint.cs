namespace Xplorium.Web.Routing {
    using System.Web;
    using System.Web.Routing;

    public class UInt32RouteConstraint : IRouteConstraint {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection) {
            try {
                uint result;
                if (uint.TryParse(values[parameterName].ToString(), out result))
                    return true;
                return false;
            } catch {
                return false;
            }
        }
    }
}