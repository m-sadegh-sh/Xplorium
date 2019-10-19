namespace Xplorium.Web {
    using System.Text.RegularExpressions;

    public class HtmlMinifier {
        public string Minify(string input) {
            //input = Regex.Replace(input, PreparedExpressions.HtmlWhitespace, " ");
            //input = Regex.Replace(input, PreparedExpressions.RemoveAllWhiteSpaces, " ");
            input = Regex.Replace(input, @"\n|\t", " ");
            input = Regex.Replace(input, @">\s+<", "><");
            input = Regex.Replace(input, @"\s{2,}", " ");

            return input;
        }
    }
}