namespace Xplorium.Models {
    public class SearchResults {
        public SearchOption Option { get; set; }
        public string Q { get; set; }
        public PagedList<SearchResult> Results { get; set; }
        public string ElapsedTime { get; set; }
        public string State { get; set; }

        public SearchResults() {
            Option = new SearchOption();
        }
    }
}