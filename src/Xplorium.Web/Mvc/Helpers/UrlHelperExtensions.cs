namespace Xplorium.Web.Mvc {
    using System.Web.Mvc;

    public static class UrlHelperExtensions {
        private static readonly string stylesPath = "statics/styles/";
        private static readonly string scriptsPath = "statics/scripts/";

        public static string StyleSheetPath(this UrlHelper urlHelper, string fileName, bool asAbsolute = false) {
            var result = asAbsolute ? ServerPath(urlHelper) : string.Empty;
            result += urlHelper.Content(string.Concat(stylesPath, fileName));
            return result;
        }

        public static string JavaScriptPath(this UrlHelper urlHelper, string fileName, bool asAbsolute = false) {
            var result = asAbsolute ? ServerPath(urlHelper) : string.Empty;
            result += urlHelper.Content(string.Concat(scriptsPath, fileName));
            return result;
        }

        private static string ServerPath(UrlHelper urlHelper) {
            var serverPath = urlHelper.RequestContext.HttpContext.Request.Url.Scheme;
            serverPath += "://";
            serverPath += urlHelper.RequestContext.HttpContext.Request.Url.Host;
            serverPath += "/";
            return serverPath;
        }
    }
}