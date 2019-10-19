namespace Xplorium.WSE {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Xplorium.Common;
    using Xplorium.Core;
    using Xplorium.Models;
    using Xplorium.Repositories;

    public class Searcher : RepositoryBase {
        private readonly Stopwatch _watcher;
        public string Summary { get; private set; }

        public Searcher() {
            _watcher = new Stopwatch();
        }

        public IList<SearchResult> Search(string rawQuery) {
            return Search(rawQuery, KeywordParts.Anywhere);
        }

        public IList<SearchResult> Search(string rawQuery, KeywordParts part) {
            _watcher.Start();

            if (string.IsNullOrWhiteSpace(rawQuery))
                return null;

            var breakedQuery = StringExtensions.BreakQuery(rawQuery);
            if (breakedQuery == null)
                return null;

            var results = (from urls in DataContext.Urls
                           join caches in DataContext.Caches on urls.UrlId equals caches.CacheId
                           join parsedContents in DataContext.ParsedContents on caches.CacheId equals parsedContents.ParsedContentId
                           let words = (from words in DataContext.Words
                                        join hits in DataContext.Hits on words.WordId equals hits.WordId
                                        where hits.ParsedContentId == parsedContents.ParsedContentId
                                        select words.Text)
                           where words.Any(word => word.Contains(rawQuery)) // &&
                           //urls.ResolvedPath.Contains(breakedQuery.ElementAt(1).Replace("host:", string.Empty))
                           //where words.Any(word => new Comparer().WordInQueries(word, breakedQuery)) &&
                           //new Comparer().HostInQueries(urls.ResolvedPath, breakedQuery)
                           select new SearchResult {
                               UrlId = urls.UrlId,
                               Url = urls.ResolvedPath,
                               IndexedOn = parsedContents.ParsedOn,
                               Title = parsedContents.Title,
                               Description = parsedContents.Description,
                               Host = new Uri(urls.ResolvedPath).Host,
                               Length = caches.Length,
                               Rate = new Rater().Rate(breakedQuery, urls.ResolvedPath, parsedContents.Title, parsedContents.Keywords, parsedContents.Description, words)
                           }).AsEnumerable().OrderByDescending(result => result.Rate).Distinct(new SearchResultEqualityComparer()).ToList();

            _watcher.Stop();
            Summary = _watcher.Elapsed.ElapsedTime();
            return results;
        }
    }
}