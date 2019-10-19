namespace Xplorium.Core {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public static class EnumExtensions {
        public static SelectList ToSelectList(this Enum source) {
            var names = Enum.GetNames(source.GetType());
            var pairs = new List<SelectListItem>();

            foreach (var name in names) {
                pairs.Add(new SelectListItem {
                    Text = name,
                    Value = name
                });
            }

            return new SelectList(pairs, "Value", "Text", source.ToString());
        }
    }
}