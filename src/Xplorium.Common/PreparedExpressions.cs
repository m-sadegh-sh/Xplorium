namespace Xplorium.Common {
    public class PreparedExpressions {
        public static string SpecialCharacters {
            get { return @"[^a-z0-9,.]"; }
        }

        public static string AlphabeticalCharactersAndNumbers {
            get { return @"[^a-z0-9]"; }
        }

        public static string WhiteSpaces {
            get { return @"\s+"; }
        }

        public static string TitleTag {
            get { return @"(?<=<title[^\>]*>).*?(?=</title>)"; }
        }

        public static string MetaTags {
            get { return @"<meta\s*(?:(?:\b(\w|-)+\b\s*(?:=\s*(?:""[^""]*""|'[^']*'|[^""'<> ]+)\s*)?)*)/?\s*>"; }
        }

        public static string ScriptTags {
            get { return @"<script[^>.]*>[\s\S]*?</script>"; }
        }

        public static string StyleTags {
            get { return @"<style[^>.]*>[\s\S]*?</style>"; }
        }

        public static string ATagAttributes {
            get { return @"(?<name>\b\w+\b)\s*=\s*(""(?<value>[^""]*)""|'(?<value>[^']*)'|(?<value>[^""'<> \s]+)\s*)+"; }
        }

        public static string MetaTagAttributes {
            get { return @"(?<name>\b(\w|-)+\b)\s*=\s*(""(?<value>[^""]*)""|'(?<value>[^']*)'|(?<value>[^""'<> ]+)\s*)+"; }
        }

        public static string CommentTags {
            get { return @"<!(?:--[\s\S]*?--\s*)?>"; }
        }

        public static string ATags {
            get { return @"(?<anchor><\s*(a|area|frame|iframe)\s*(?:(?:\b\w+\b\s*(?:=\s*(?:""[^""]*""|'[^']*'|[^""'<> ]+)\s*)?)*)?\s*>)"; }
        }

        public static string HtmlTags {
            get { return @"<(.|\n)*?>"; }
        }

        public static string HtmlAllowedWhitespaces {
            get { return @"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}"; }
        }

        public static string HtmlEncodedCharacters {
            get { return @"&[#;a-z0-9]+;"; }
        }

        public static string HtmlIllegalCharacters {
            get { return @"[^a-zA-Z0-9$%][^a-zA-Z0-9$%]*[^a-zA-Z0-9$%]"; }
        }
    }
}