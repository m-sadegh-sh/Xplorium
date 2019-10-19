namespace Xplorium.Web.Mvc {
    using System.Web.Mvc;

    using Xplorium.Web.Routing;

    public class SearchAreaRegistration : AreaRegistration {
        public override string AreaName {
            get { return "Search"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute("SearchWeb", "{culture}/{style}/search/web/", new {
                Controller = "Web",
                Action = "Default",
                Culture = "en-gb",
                Style = "zune"
            }, new {
                Culture = new CultureRouteConstraint(),
                Style = new StyleRouteConstraint()
            });

            context.MapRoute("SearchWebAdvanced", "{culture}/{style}/search/web/advanced/", new {
                Controller = "Web",
                Action = "Advanced",
                Culture = "en-gb",
                Style = "zune"
            }, new {
                Culture = new CultureRouteConstraint(),
                Style = new StyleRouteConstraint()
            });

            context.MapRoute("SearchWebJsonSuggestion", "{culture}/{style}/search/web/suggest/", new {
                Controller = "Web",
                Action = "JsonSuggestion",
                Culture = "en-gb",
                Style = "zune"
            }, new {
                Culture = new CultureRouteConstraint(),
                Style = new StyleRouteConstraint()
            });

            context.MapRoute("SearchWebQueryAnalyze", "{culture}/{style}/search/web/query-analyze/", new {
                Controller = "Web",
                Action = "QueryAnalyze",
                Culture = "en-gb",
                Style = "zune"
            }, new {
                Culture = new CultureRouteConstraint(),
                Style = new StyleRouteConstraint()
            });

            context.MapRoute("SearchWebTracker", "{culture}/{style}/search/web/tracker/{UrlId}/", new {
                Controller = "Web",
                Action = "Redirect",
                Culture = "en-gb",
                Style = "zune"
            }, new {
                Culture = new CultureRouteConstraint(),
                Style = new StyleRouteConstraint(),
                UrlId = new GuidRouteConstraint()
            });

            context.MapRoute("SearchWebCache", "{culture}/{style}/search/web/cache/{CacheId}/", new {
                Controller = "Web",
                Action = "Cache",
                Culture = "en-gb",
                Style = "zune"
            }, new {
                Culture = new CultureRouteConstraint(),
                Style = new StyleRouteConstraint(),
                CacheId = new GuidRouteConstraint()
            });
        }
    }
}