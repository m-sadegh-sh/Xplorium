namespace Xplorium.Windows {
    using System;
    using System.Security.Permissions;

    [Serializable, PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    internal class InstanceProxy : MarshalByRefObject {
        public static bool IsFirstInstance { get; internal set; }
        public static string[] CommandLineArgs { get; internal set; }

        public void SetCommandLineArgs(bool isFirstInstance, string[] commandLineArgs) {
            IsFirstInstance = isFirstInstance;
            CommandLineArgs = commandLineArgs;
        }
    }
}