namespace Xplorium.Web.Routing {
    using System.Web;
    using System.Web.Routing;

    public class CultureRouteConstraint : IRouteConstraint {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection) {
            try {
                var parameter = values[parameterName] as string;
                return true; // (EnumExtensions.IsValidAmbientValue(typeof(Culture), parameter));
            } catch {
                return false;
            }
        }
    }
}