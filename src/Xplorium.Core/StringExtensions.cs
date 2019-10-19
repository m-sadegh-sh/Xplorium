namespace Xplorium.Core {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using Xplorium.Common;

    public static class StringExtensions {
        private static Uri _temp;

        private static readonly IList<string> ReservedTags = new List<string> {
            "b",
            "i"
        };

        public static bool Contains(this string source, string value, StringComparison comparisonType) {
            if (!string.IsNullOrWhiteSpace(source) && !string.IsNullOrWhiteSpace(value))
                return (source.IndexOf(value, comparisonType) >= 0);
            return false;
        }

        public static bool Contains(this string source, IList<string> values, KeywordParts valuePart, KeywordParts part) {
            return !string.IsNullOrWhiteSpace(source) && source.Split(' ').ToList().Contains(values, valuePart, part);
        }

        public static bool Contains(this string source, IList<string> items) {
            return items.Any(item => source.Contains(item, StringComparison.CurrentCulture));
        }

        public static string Compress(this string flatText) {
            var buffer = Encoding.UTF8.GetBytes(flatText);
            var ms = new MemoryStream();
            using (var zip = new GZipStream(ms, CompressionMode.Compress, true))
                zip.Write(buffer, 0, buffer.Length);

            ms.Position = 0;
            var compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            var gzBuffer = new byte[compressed.Length + 4];
            Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return Convert.ToBase64String(gzBuffer);
        }

        public static string Decompress(this string compressedText) {
            var gzBuffer = Convert.FromBase64String(compressedText);
            using (var ms = new MemoryStream()) {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                var buffer = new byte[msgLength];

                ms.Position = 0;
                using (var zip = new GZipStream(ms, CompressionMode.Decompress))
                    zip.Read(buffer, 0, buffer.Length);

                return Encoding.UTF8.GetString(buffer);
            }
        }

        public static string Substring(this string source, int startIndex, int length, bool appendThreeDotSign) {
            if (string.IsNullOrWhiteSpace(source))
                return null;
            startIndex = startIndex < 0 ? 0 : startIndex;
            length = length < 0 || startIndex + length > source.Length ? source.Length - startIndex : length;
            if (appendThreeDotSign)
                length -= 1;
            return (source.Length > length) ? source.Substring(startIndex, length) + (appendThreeDotSign ? "…" : string.Empty) : source;
        }

        public static IList<string> ToList(string input) {
            var output = StripHtml(input);
            output = Regex.Replace(output, PreparedExpressions.WhiteSpaces, " ");
            return output.Split(' ').Distinct().ToList();
        }

        public static IList<string> BreakQuery(string rawQuery) {
            return string.IsNullOrWhiteSpace(rawQuery) ? null : rawQuery.Split(' ').Distinct().ToList();
        }

        public static string CombineQueries(string q, string none, string host) {
            IList<string> qs = null;
            IList<string> nones = null;

            if (!string.IsNullOrWhiteSpace(q))
                qs = q.Split(' ').Distinct().Select(n => string.Format("{0} ", n)).ToList();

            if (!string.IsNullOrWhiteSpace(none))
                nones = none.Split(' ').Distinct().Select(n => string.Format("{0}{1} ", (n.StartsWith("-")) ? string.Empty : "-", n)).ToList();

            var results = string.Empty;

            if (qs != null)
                results = qs.Aggregate(results, (current, _q) => current + _q);

            if (nones != null)
                results = nones.Aggregate(results, (current, _n) => current + _n);

            if (!string.IsNullOrWhiteSpace(host))
                results += "host:" + host;

            return results.Trim();
        }

        public static string ResolveUrl(string path, Uri baseUri) {
            try {
                if (path.StartsWith("javascript:") || path.StartsWith("mailto:") || path.StartsWith("#"))
                    return string.Empty;

                var temp = new Uri(baseUri, path);

                temp = AppendWwwIfNeeded(temp);

                return temp.AbsoluteUri;
            } catch {
                return string.Empty;
            }
        }

        private static Uri AppendWwwIfNeeded(Uri temp) {
            var count = temp.Host.Count(t => t == '.');

            var host = temp.Host;

            if (count == 1)
                host = string.Concat("www.", host);

            var final = string.Concat(temp.Scheme, "://");
            final += host;
            final += temp.AbsolutePath;

            return new Uri(final);
        }

        public static string StripHtml(string html) {
            const RegexOptions regexOptions = RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled;

            var regex = new Regex(PreparedExpressions.ScriptTags, regexOptions);
            var plain = regex.Replace(html, " ");

            regex = new Regex(PreparedExpressions.StyleTags, regexOptions);
            plain = regex.Replace(plain, " ");

            regex = new Regex(PreparedExpressions.CommentTags, regexOptions);
            plain = regex.Replace(plain, " ");

            regex = new Regex(PreparedExpressions.HtmlTags, regexOptions);
            plain = regex.Replace(plain, Recognize);

            regex = new Regex(PreparedExpressions.HtmlEncodedCharacters, regexOptions);
            plain = regex.Replace(plain, string.Empty);

            regex = new Regex(PreparedExpressions.HtmlIllegalCharacters, regexOptions);
            plain = regex.Replace(plain, " ");

            regex = new Regex(PreparedExpressions.WhiteSpaces, regexOptions);
            plain = regex.Replace(plain, " ");

            plain = plain.Trim();

            return plain;
        }

        private static string Recognize(Match match) {
            var tag = GetTagName(match.ToString());
            return tag.Contains(ReservedTags) ? string.Empty : " ";
        }

        public static string GetTagName(string source) {
            int startPoint;
            int endPoint;

            if (IsClosedTag(source))
                startPoint = source.IndexOf("</") + 2;
            else
                startPoint = source.IndexOf("<") + 1;

            endPoint = source.IndexOf(" ");
            if (endPoint == -1) {
                if (IsSelfClosedTag(source))
                    endPoint = source.EndsWith(" />") ? source.IndexOf(" />") : source.LastIndexOf("/>");
                else if (source.EndsWith(" >"))
                    endPoint = source.IndexOf(" >");
                else
                    endPoint = source.LastIndexOf(">");
            }

            var tagName = source.Substring(startPoint, endPoint - 1).ToLowerInvariant();
            return tagName;
        }

        public static bool IsClosedTag(string tag) {
            return (tag.StartsWith("</"));
        }

        public static bool IsSelfClosedTag(string tag) {
            return (tag.EndsWith("/>"));
        }

        public static string StripUrl(string input) {
            return input.Substring(0, input.IndexOf('#') > -1 ? input.IndexOf('#') : input.Length);
        }

        public static string ToSlug(this string helper) {
            helper = helper.ToLower();
            helper = Regex.Replace(helper, @"&\w+;", "");
            helper = Regex.Replace(helper, @"[^a-z0-9\-\s]", "");
            helper = Regex.Replace(helper, " ", "-");
            helper = Regex.Replace(helper, @"-{2,}", "-");
            helper = helper.TrimStart(new[] {'-'});
            helper = helper.TrimEnd(new[] {'-'});
            return helper;
        }

        public static string RemoveNonStandardCharacters(string source) {
            return Regex.Replace(source, PreparedExpressions.SpecialCharacters, " ");
        }

        public static string ToStrong(this string source, string pattern) {
            //source.ThrowNullOrWhiteSpace();
            //pattern.ThrowNullOrWhiteSpace();
            var result = source;
            //foreach (Match match in Regex.Matches(source, pattern, RegexOptions.IgnoreCase))
            //result = Regex.Replace(result, match.ToString(), "<strong>" + match + "</strong>", RegexOptions.ExplicitCapture);
            return result;
        }

        public static bool UrlIsValid(string input) {
            return Uri.TryCreate(input, UriKind.RelativeOrAbsolute, out _temp) && input.Length <= 2048;
        }
    }
}