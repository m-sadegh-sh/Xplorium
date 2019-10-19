namespace Xplorium.Web.Mvc {
    using System;
    using System.Data.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class XploriumApplication : HttpApplication {
        protected void Application_Start() {
            RouteTable.Routes.Clear();
            AreaRegistration.RegisterAllAreas();
            RouteRegistration.RegisterAllRoutes();

            ModelBinders.Binders.Clear();
            ModelBinders.Binders.Add(typeof (Binary), new LinqBinaryModelBinder());
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            //HttpHandler httpHandler = new HttpHandler(this.Context);
            //httpHandler.ProcessRequest();
        }
    }
}