namespace Xplorium.Web.Mvc {
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public class XploriumExtensions {
        private readonly HtmlHelper _htmlHelper;

        public XploriumExtensions(HtmlHelper htmlHelper) {
            _htmlHelper = htmlHelper;
        }

        public string Username() {
            return Authenticated() ? HttpContext.Current.User.Identity.Name : "Anonymous";
        }

        public bool Authenticated() {
            return (HttpContext.Current.User.Identity.IsAuthenticated);
        }

        public string Account() {
            return string.Format("<li><span>Welcome <strong>{0}</strong></span></li><li>{1}</li><li>{2}</li>", Username(), Authenticated() ? _htmlHelper.RouteLink("Profile", "").ToString() : _htmlHelper.RouteLink("Register", "").ToString(), Authenticated() ? _htmlHelper.RouteLink("Log Out", "MembershipAuthenticateLogOut").ToString() : _htmlHelper.RouteLink("Log In", "MembershipAuthenticateLogIn").ToString());
        }
    }

    public static class HtmlHelperExtensions {
        public static string StyleSheetBlock(this HtmlHelper htmlHelper, string fileName, bool asAbsolute = true) {
            return string.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", new UrlHelper(htmlHelper.ViewContext.RequestContext).StyleSheetPath(fileName, asAbsolute));
        }

        public static string StyleSheetBlock(this HtmlHelper htmlHelper, List<string> fileNames, bool asAbsolute = true) {
            var results = new StringBuilder();
            foreach (string fileName in fileNames)
                results.Append(StyleSheetBlock(htmlHelper, fileName, asAbsolute));
            return results.ToString();
        }

        public static string JavaScriptBlock(this HtmlHelper htmlHelper, string fileName, bool asAbsolute = true) {
            return string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", new UrlHelper(htmlHelper.ViewContext.RequestContext).JavaScriptPath(fileName, asAbsolute));
        }

        public static string JavaScriptBlock(this HtmlHelper htmlHelper, List<string> fileNames, bool asAbsolute = true) {
            var results = new StringBuilder();
            foreach (string fileName in fileNames)
                results.Append(JavaScriptBlock(htmlHelper, fileName, asAbsolute));
            return results.ToString();
        }

        public static XploriumExtensions Xplorium(this HtmlHelper htmlHelper) {
            return new XploriumExtensions(htmlHelper);
        }
    }
}