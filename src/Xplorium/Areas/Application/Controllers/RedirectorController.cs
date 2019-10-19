namespace Xplorium.Web.Mvc
{
    using System.Web.Mvc;

    public class RedirectorController : ExtendedController
    {
        public ActionResult ToSearchWeb()
        {
            return RedirectToRoute("SearchWeb");
        }
    }
}
