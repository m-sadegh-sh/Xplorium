namespace Xplorium.Web.Mvc
{
    using System.Web.Mvc;

    public class StaticsController : ExtendedController
    {
        public ActionResult CssRender(string relativePath)
        {
            return Content(relativePath, OptimizeMode.Css);
        }

        public ActionResult JsRender(string relativePath)
        {
            return Content(relativePath, OptimizeMode.Js);
        }
    }
}
