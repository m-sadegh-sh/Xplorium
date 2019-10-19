namespace Xplorium.WSE {
    using System.Collections.Generic;
    using System.Linq;

    public class Rater {
        public int Rate(IEnumerable<string> breakedQuery, string resolvedPath, string title, string keywords, string description, IEnumerable<string> words) {
            var baseRate = 0;

            foreach (var query in breakedQuery) {
                var none = (query.StartsWith("-"));
                var host = (query.StartsWith("host:"));
                var term = query.Replace("-", "");
                var result = 0;
                if (!host) {
                    var pathCount = Calculate(resolvedPath, term);
                    var titleCount = Calculate(title, term);
                    var keywordsCount = Calculate(keywords, term);
                    var descriptionCount = Calculate(description, term);
                    var wordsCount = Calculate(words, term);
                    result = (pathCount*100) + (titleCount*50) + (keywordsCount*25) + (descriptionCount*10) + (wordsCount);
                }

                if (none)
                    baseRate -= result;
                else
                    baseRate += result;
            }
            return baseRate;
        }

        private static int Calculate(string source, string query) {
            return !string.IsNullOrWhiteSpace(source) ? Calculate(source.Split(' ').AsEnumerable(), query) : 0;
        }

        private static int Calculate(IEnumerable<string> sources, string query) {
            var count = 0;
            if (sources != null && sources.Count() > 0) {
                var elements = sources.Where(source => source == query);
                count += elements.Count();
                count += sources.Except(elements).Where(source => source.ToLowerInvariant() == query.ToLowerInvariant()).Count()/2;
            }
            return count;
        }
    }
}