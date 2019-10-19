namespace Xplorium.Web.Mvc {
    using System.Web.Mvc;
    using System.Web.Routing;

    using Xplorium.Web.Routing;

    public static class RouteRegistration {
        public static void RegisterAllRoutes() {
            RegisterAllRoutes(RouteTable.Routes);
        }

        public static void RegisterAllRoutes(RouteCollection routes) {
            routes.RouteExistingFiles = true;

            //routes.IgnoreRoute("statics/images/{*}");
            routes.MapRoute("Home", "{Culture}/{Style}/home/", new {
                Culture = "en-gb",
                Style = "zune",
                Controller = "Home",
                Action = "Default"
            }, new {
                Culture = new CultureRouteConstraint(),
                Style = new StyleRouteConstraint()
            });

            routes.MapRoute("NotFound", "{PathInfo}", new {
                Controller = "Libraries",
                Action = "Error"
            });
        }
    }
}