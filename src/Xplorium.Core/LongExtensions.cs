namespace Xplorium.Core {
    using System;

    public static class LongExtensions {
        public static string FormatBytes(this long bytes) {
            const int scale = 1024;
            var orders = new[] {"GB", "MB", "KB", "Bytes"};
            var max = (long) Math.Pow(scale, orders.Length - 1);

            foreach (string order in orders) {
                if (bytes > max)
                    return string.Format("{0:##.##} {1}", decimal.Divide(bytes, max), order);
                max /= scale;
            }
            return "0 Bytes";
        }
    }
}