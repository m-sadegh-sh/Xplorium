namespace Xplorium.Web.Mvc {
    using System.Web.Mvc;

    using Xplorium.Web.Routing;

    public class MembershipAreaRegistration : AreaRegistration {
        public override string AreaName {
            get { return "Membership"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute("MembershipAuthenticateLogIn", "{culture}/{style}/membership/sign-in/", new {
                Controller = "Authenticate",
                Action = "LogIn",
                Culture = "en-gb",
                Style = "zune"
            }, new {
                Culture = new CultureRouteConstraint(),
                Style = new StyleRouteConstraint()
            });

            context.MapRoute("MembershipAuthenticateLogOut", "{culture}/{style}/membership/sign-out/", new {
                Controller = "Authenticate",
                Action = "LogOut",
                Culture = "en-gb",
                Style = "zune"
            }, new {
                Culture = new CultureRouteConstraint(),
                Style = new StyleRouteConstraint()
            });
        }
    }
}