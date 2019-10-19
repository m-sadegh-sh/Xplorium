namespace Xplorium.Models {
    using System.Collections.Generic;

    public class SearchResultEqualityComparer : IEqualityComparer<SearchResult> {
        public bool Equals(SearchResult x, SearchResult y) {
            return (x.Url == y.Url);
        }

        public int GetHashCode(SearchResult obj) {
            return obj.Url.GetHashCode();
        }
    }
}