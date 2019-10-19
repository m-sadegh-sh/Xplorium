namespace Xplorium.Web.Mvc {
    using System.Web.Mvc;

    using Xplorium.Core;

    public class OptimizeAttribute : ActionFilterAttribute {
        public bool Compress { get; set; }
        public bool Minify { get; set; }

        public OptimizeAttribute(bool compress = true, bool minify = true) {
            Compress = compress;
            Minify = minify;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext) {
            if (!CheckArgument.IsNull(() => filterContext.Exception))
                return;

            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;

            var optimizeMode = ResolveContentType(response.ContentType);

            var canBeCompressed = ((optimizeMode == OptimizeMode.Html && Preferences.EnableHtmlCompression) || (optimizeMode == OptimizeMode.Css && Preferences.EnableCssCompression) || (optimizeMode == OptimizeMode.Js && Preferences.EnableJsCompression));

            var canBeMinified = ((optimizeMode == OptimizeMode.Html && Preferences.EnableHtmlMinification) || (optimizeMode == OptimizeMode.Css && Preferences.EnableCssMinification) || (optimizeMode == OptimizeMode.Js && Preferences.EnableJsMinification));

            string acceptEncoding = (request.Headers["Accept-Encoding"] ?? string.Empty).ToLower();

            if (canBeMinified)
                response.Filter = new MinifierStream(response.Filter, optimizeMode);

            //if (acceptEncoding.Contains("gzip"))
            //{
            //    response.AddHeader("Content-Encoding", "gzip");
            //    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            //}
            //else if (acceptEncoding.Contains("deflate"))
            //{
            //    response.AddHeader("Content-Encoding", "deflate");
            //    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            //}
        }

        private OptimizeMode ResolveContentType(string contentType) {
            switch (contentType) {
                case "text/htm":
                case "text/html":
                    return OptimizeMode.Html;
                case "text/css":
                    return OptimizeMode.Css;
                case "text/javascript":
                    return OptimizeMode.Js;
                default:
                    return OptimizeMode.None;
            }
        }
    }
}