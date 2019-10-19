namespace Xplorium.WSE {
    using System;
    using System.Configuration;

    public static class Preferences {
        private static int IfNullDefault(string appSetting, int defaultValue) {
            return ConfigurationManager.AppSettings[appSetting] == null ? defaultValue : int.Parse(ConfigurationManager.AppSettings[appSetting]);
        }

        private static bool IfNullDefault(string appSetting, bool defaultValue) {
            return ConfigurationManager.AppSettings[appSetting] == null ? defaultValue : bool.Parse(ConfigurationManager.AppSettings[appSetting]);
        }

        private static BadWordMode IfNullDefault(string appSetting, BadWordMode defaultValue) {
            return ConfigurationManager.AppSettings[appSetting] == null ? defaultValue : (BadWordMode) Enum.Parse(typeof (BadWordMode), ConfigurationManager.AppSettings[appSetting], true);
        }

        private static string IfNullDefault(string appSetting, string defaultValue) {
            return ConfigurationManager.AppSettings[appSetting] ?? defaultValue;
        }

        public static int RequestTimeout {
            get { return IfNullDefault("RequestTimeout", 30); }
        }

        public static int MaximumRecursionTimes {
            get { return IfNullDefault("MaximumRecursionTimes", 10000); }
        }

        public static int MaximumFailureAttempts {
            get { return IfNullDefault("MaximumFailureAttempts", 10000); }
        }

        public static BadWordMode BadWordCleaningMode {
            get { return IfNullDefault("BadWordCleaningMode", BadWordMode.NoCleaning); }
        }

        public static string UserAgent {
            get { return IfNullDefault("UserAgent", "Xplorium.NET Spider"); }
        }

        public static string LogFilePath {
            get { return "C:\\Log.log"; }
        }
    }
}