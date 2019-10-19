namespace Xplorium.Web.Mvc {
    using System;
    using System.Web.Mvc;

    using Xplorium.Common;

    public class KeywordPartAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            const string keywordPartKey = "part";
            if (filterContext.ActionParameters.ContainsKey(keywordPartKey)) {
                var part = filterContext.HttpContext.Request.QueryString[keywordPartKey];
                KeywordParts keywordPart;
                if (Enum.TryParse(part, out keywordPart))
                    filterContext.ActionParameters[keywordPartKey] = keywordPart;
                else
                    filterContext.ActionParameters[keywordPartKey] = KeywordParts.Anywhere;
            }
        }
    }
}