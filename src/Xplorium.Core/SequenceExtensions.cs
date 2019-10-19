namespace Xplorium.Core {
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Xplorium.Common;

    public static class SequenceExtensions {
        public static SelectList ToSelectList<T>(this IList<T> source, bool useSelfAsValue = false) {
            var i = 0;
            var pairs = source.Select(item => new SelectListItem {
                Text = item.ToString(),
                Value = (i == 0) ? string.Empty : (useSelfAsValue ? item.ToString() : (i++).ToString())
            }).ToList();

            return new SelectList(pairs, "Value", "Text", source.First().ToString());
        }

        public static bool Contains(this IList<string> source, IList<string> values, KeywordParts valuePart, KeywordParts part) {
            if (source != null && source.Count() > 0 && values != null && values.Count() > 0 && (part == KeywordParts.Anywhere || valuePart == part)) {
                return (from value in values
                        let has = false
                        let none = (value.StartsWith("-"))
                        let term = value.Replace("-", "")
                        select none ? source.Any(q => !q.Contains(value)) : source.Any(q => q.Contains(values))).FirstOrDefault(has => has);
            }
            return false;
        }
    }
}