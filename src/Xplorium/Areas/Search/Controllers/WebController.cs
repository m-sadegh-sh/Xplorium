namespace Xplorium.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Xplorium.Common;
    using Xplorium.Core;
    using Xplorium.Models;
    using Xplorium.Repositories;
    using Xplorium.WSE;

    public class WebController : ExtendedController
    {
        [KeywordPart]
        public ActionResult Default(string q, string none, string host, int start = 0, int size = 10, KeywordParts part = KeywordParts.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(q) &&
                string.IsNullOrWhiteSpace(none) &&
                string.IsNullOrWhiteSpace(host))
                return Basic();

            var combinedQuery = StringExtensions.CombineQueries(q, none, host);

            if (string.IsNullOrWhiteSpace(combinedQuery))
                return Basic();

            return Result(combinedQuery, start, size, part);
        }

        private ActionResult Basic()
        {
            ViewData["State"] = string.Format(Web.Preferences.Messages.Summaries,
                new ParsedContentRepository().Count(),
                new CacheRepository().Count(),
                new UrlRepository().Count(),
                new WordRepository().Count(),
                new HitRepository().Count());

            return View("Basic");
        }

        public ActionResult Advanced()
        {
            return View("Advanced", new SearchOption());
        }

        public ActionResult JsonSuggestion(string term)
        {
            IEnumerable<string> results;
            if (string.IsNullOrWhiteSpace(term))
                results = new XploriumDataContext().Words.Select(word => word.Text).Take(7).AsEnumerable();
            else
                results = new XploriumDataContext().Words.Where(word => word.Text.Contains(term)).Select(word => word.Text).Take(7).AsEnumerable();
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QueryAnalyze(string q, string none)
        {
            var result = StringExtensions.CombineQueries(q, none, string.Empty);
            return Content(result);
        }

        private ActionResult Result(string q, int start, int size, KeywordParts part)
        {
            ViewData["q"] = q;
            var searcher = new Searcher();
            var results = searcher.Search(q, part).AsQueryable().ToPagedList(start, size);
            ViewData["Summary"] = searcher.Summary;

            if (results == null || results.Count() == 0)
                return View("Not-Found");
            if (Request.IsAjaxRequest())
                return PartialView("Partials/Results", results);
            return View("Result", results);
        }

        public ActionResult Redirect(Guid urlId)
        {
            var urlRepository = new UrlRepository();
            var url = urlRepository.Get(urlId);
            if (url == null)
                return Content("Invalid UrlId");
            return Redirect(url.ResolvedPath);
        }

        public ActionResult Cache(Guid cacheId)
        {
            var cacheRepository = new CacheRepository();
            var cache = cacheRepository.Get(cacheId);
            return Content(cache == null ? "Invalid CacheId" : cache.Response.Decompress());
        }
    }
}
