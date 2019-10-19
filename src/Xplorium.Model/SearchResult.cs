namespace Xplorium.Models {
    using System;

    public class SearchResult {
        public Guid UrlId { get; set; }
        public string Url { get; set; }
        public DateTime IndexedOn { get; set; }
        public string Title { get; set; }
        public string Host { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public long Length { get; set; }
    }
}