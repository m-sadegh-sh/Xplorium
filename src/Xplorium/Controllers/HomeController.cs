namespace Xplorium.Web.Mvc
{
    using System.Web.Mvc;

    public class HomeController : ExtendedController
    {
        public ActionResult Default()
        {
            return View();
        }
    }
}
