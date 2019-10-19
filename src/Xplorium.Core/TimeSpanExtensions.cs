namespace Xplorium.Core {
    using System;
    using System.Collections.Generic;

    public static class TimeSpanExtensions {
        public static string ElapsedTime(this TimeSpan took) {
            var strings = new List<string>();

            var parts = new List<int> {
                took.Days,
                took.Hours,
                took.Minutes,
                took.Seconds
            };
            var units = new List<string> {
                "day",
                "hour",
                "minute",
                "second"
            };

            foreach (var part in parts) {
                if (part > 0)
                    strings.Add(string.Format("{0} {1}", part, Pluralize(part, units[parts.IndexOf(part)])));
            }

            return "About " + (strings.Count != 0 ? string.Join(", ", strings.ToArray()) : "0 seconds");
        }

        private static string Pluralize(int n, string unit) {
            if (string.IsNullOrWhiteSpace(unit))
                return string.Empty;

            n = Math.Abs(n);

            return unit + (n == 1 ? string.Empty : "s");
        }
    }
}