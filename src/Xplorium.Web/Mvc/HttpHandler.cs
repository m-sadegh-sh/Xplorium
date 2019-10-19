namespace Xplorium.Web.Mvc {
    using System.Text.RegularExpressions;
    using System.Web;

    using Xplorium.Globalization;

    public class HttpHandler // : System.Web.IHttpHandler
    {
        private readonly HttpContext httpContext;

        public HttpHandler(HttpContext httpContext) {
            this.httpContext = httpContext;
        }

        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest() {
            string lowercaseURL = string.Format(Format.Url, httpContext.Request.Url.Scheme, httpContext.Request.Url.Authority, httpContext.Request.Url.AbsolutePath);
            if (Regex.IsMatch(lowercaseURL, @"[A-Z]")) {
                lowercaseURL = lowercaseURL.ToLower() + httpContext.Request.Url.Query;
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = 301;
                httpContext.Response.StatusDescription = "Moved Permanently";
                httpContext.Response.AddHeader("Location", lowercaseURL);
                httpContext.Response.End();
            }
        }
    }
}