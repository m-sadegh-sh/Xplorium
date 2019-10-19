namespace Xplorium.Web.Mvc {
    using System.Web.Mvc;

    using Xplorium.Web.Routing;

    public class ApplicationAreaRegistration : AreaRegistration {
        public override string AreaName {
            get { return "Application"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute("ApplicationRedirectorToSearchWeb", "", new {
                Controller = "Redirector",
                Action = "ToSearchWeb"
            });

            context.MapRoute("ApplicationLibrariesError", "{Culture}/{Style}/libraries/error/{PathInfo}/", new {
                Controller = "Libraries",
                Action = "Error",
                Culture = "en-gb",
                Style = "zune"
            }, new {
                Culture = new CultureRouteConstraint(),
                Style = new StyleRouteConstraint()
            });

            context.MapRoute("ApplicationStaticsCss", "statics/styles/{*RelativePath}", new {
                Controller = "Statics",
                Action = "CssRender"
            });

            context.MapRoute("ApplicationStaticsJs", "statics/scripts/{*RelativePath}", new {
                Controller = "Statics",
                Action = "JsRender"
            });
        }
    }
}