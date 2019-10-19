namespace Xplorium.Web.Mvc
{
    using System.Web.Mvc;
    using Xplorium.WSE;

    public class LibrariesController : ExtendedController
    {
        public ActionResult Error(string pathInfo)
        {
            return Content(string.Format("Not Found {0}", pathInfo));
        }

        public ActionResult Splash()
        {
            return Content(string.Format("Welcome to {0}", Preferences.UserAgent));
        }
    }
}
