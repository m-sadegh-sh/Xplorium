namespace Xplorium.Web.Mvc
{
    using System.Web.Mvc;
    using System.Web.Security;
    using Xplorium.Core;

    public class AuthenticateController : ExtendedController
    {
        [HttpGet]
        public ActionResult LogIn()
        {
            if (Request.IsAuthenticated)
                return View("AlreadyLoggedIn");
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string username, string redirecTo)
        {
            if (Request.IsAuthenticated)
                return View("AlreadyLoggedIn");
            if (!string.IsNullOrWhiteSpace(username) && ModelState.IsValid)
            {
                FormsAuthentication.RedirectFromLoginPage(username, true);
                if (!string.IsNullOrWhiteSpace(redirecTo) && StringExtensions.UrlIsValid(redirecTo))
                    Response.Redirect(redirecTo);
                return RedirectToRoute("SearchWeb");
            }
            else
            {
                ModelState.AddModelError("username", "Username can't be empty or whitespace");
                return View();
            }
        }

        [Authorize]
        public ActionResult LogOut(string redirecTo)
        {
            if (Request.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                if (!string.IsNullOrWhiteSpace(redirecTo) && StringExtensions.UrlIsValid(redirecTo))
                    Response.Redirect(redirecTo);
            }
            return RedirectToRoute("SearchWeb");
        }
    }
}
