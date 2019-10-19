namespace Xplorium.Models {
    using System.Web.Mvc;

    public class SearchOption {
        public SelectList MimeType { get; set; }
        public SelectList Encoding { get; set; }
    }
}