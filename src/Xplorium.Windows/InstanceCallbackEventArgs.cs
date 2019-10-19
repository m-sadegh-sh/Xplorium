namespace Xplorium.Windows {
    using System;

    public class InstanceCallbackEventArgs : EventArgs {
        public bool IsFirstInstance { get; private set; }
        public string[] CommandLineArgs { get; private set; }

        internal InstanceCallbackEventArgs(bool isFirstInstance, string[] commandLineArgs) {
            IsFirstInstance = isFirstInstance;
            CommandLineArgs = commandLineArgs;
        }
    }
}