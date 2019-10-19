namespace Xplorium.Web.Mvc {
    using System.IO;
    using System.Web.Mvc;

    using Xplorium.Models;

    [Optimize]
    [Path]
    //[OutputCache(CacheProfile = "DefaultCacheProfile")]
    [HandleError(View = "Internal-Server-Error")]
    [HandleErrorNotFound]
    public class ExtendedController : Controller {
        protected readonly XploriumDataContext _context;

        public ExtendedController() {
            _context = new XploriumDataContext();
        }

        protected ActionResult Content(string relativePath, OptimizeMode optimizeMode) {
            if (string.IsNullOrWhiteSpace(relativePath))
                throw new NotFoundException();

            var contentType = string.Empty;
            var absolutePath = string.Empty;

            if (optimizeMode == OptimizeMode.Css)
                absolutePath = string.Format(Preferences.CssFolder, relativePath);
            else if (optimizeMode == OptimizeMode.Js)
                absolutePath = string.Format(Preferences.JsFolder, relativePath);
            absolutePath = Server.MapPath(absolutePath);

            var output = string.Empty;
            try {
                using (var reader = new StreamReader(absolutePath))
                    output = reader.ReadToEnd();
            } catch {}

            if (string.IsNullOrWhiteSpace(output))
                throw new NotFoundException();

            if (optimizeMode == OptimizeMode.Css)
                contentType = "text/css";
            else if (optimizeMode == OptimizeMode.Js)
                contentType = "text/javascript";

            return Content(output, contentType);
        }
    }
}