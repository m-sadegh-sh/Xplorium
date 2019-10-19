namespace Xplorium.Web {
    using System;
    using System.Configuration;

    public static class Preferences {
        public static class RouteKeys {
            public static string Area {
                get { return "Area"; }
            }

            public static string Controller {
                get { return "Controller"; }
            }

            public static string Action {
                get { return "Action"; }
            }

            public static string Culture {
                get { return "Culture"; }
            }

            public static string Style {
                get { return "Style"; }
            }

            public static string PathInfo {
                get { return "PathInfo"; }
            }

            public static string RelativePath {
                get { return "RelativePath"; }
            }

            public static string Q {
                get { return "Q"; }
            }
        }

        public static class Application {
            public static string Name {
                get { return "Xplorium.NET"; }
            }

            public static string Copyright {
                get { return string.Format("Copyright © {0} {1}. All rights reserved.", DateTime.Now.Year.ToString(), Name); }
            }
        }

        public static class Messages {
            public static string Summaries {
                get { return "Over <strong>{0}</strong> parsed (ready to search) contents, <strong>{1}</strong> caches, <strong>{2}</strong> urls, <strong>{3}</strong> words and <strong>{4}</strong> hits"; }
            }

            public static string ActivityIndicator {
                get { return "Please wait"; }
            }
        }

        public static class SearchEngine {
            public static string LogFilePath {
                get { return "C:\\Log.log"; }
            }
        }

        private static int IfNullDefault(string appSetting, int defaultValue) {
            return ConfigurationManager.AppSettings[appSetting] == null ? defaultValue : int.Parse(ConfigurationManager.AppSettings[appSetting]);
        }

        private static bool IfNullDefault(string appSetting, bool defaultValue) {
            return ConfigurationManager.AppSettings[appSetting] == null ? defaultValue : bool.Parse(ConfigurationManager.AppSettings[appSetting]);
        }

        private static string IfNullDefault(string appSetting, string defaultValue) {
            return ConfigurationManager.AppSettings[appSetting] ?? defaultValue;
        }

        public static bool EnableJsMinification {
            get { return IfNullDefault("EnableJsMinification", true); }
        }

        public static bool EnableJsCompression {
            get { return IfNullDefault("EnableJsCompression", true); }
        }

        public static bool EnableJsCaching {
            get { return IfNullDefault("EnableJsCaching", true); }
        }

        public static bool EnableHtmlMinification {
            get { return IfNullDefault("EnableHtmlMinification", true); }
        }

        public static bool EnableHtmlCompression {
            get { return IfNullDefault("EnableHtmlCompression", true); }
        }

        public static bool EnableHtmlCaching {
            get { return IfNullDefault("EnableHtmlCaching", true); }
        }

        public static bool EnableCssMinification {
            get { return IfNullDefault("EnableCssMinification", true); }
        }

        public static bool EnableCssCompression {
            get { return IfNullDefault("EnableCssCompression", true); }
        }

        public static bool EnableCssCaching {
            get { return IfNullDefault("EnableCssCaching", true); }
        }

        public static string CssFolder {
            get { return "/statics/styles/{0}.css"; }
        }

        public static string JsFolder {
            get { return "/statics/scripts/{0}.js"; }
        }
    }
}