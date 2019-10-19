namespace Xplorium.Windows {
    using System;
    using System.Diagnostics;
    using System.Runtime.Remoting;
    using System.Runtime.Remoting.Channels;
    using System.Runtime.Remoting.Channels.Ipc;
    using System.Threading;

    public static class InstanceManager {
        public static bool CreateSingleInstance(string name, EventHandler<InstanceCallbackEventArgs> callback) {
            EventWaitHandle eventWaitHandle = null;
            string eventName = string.Format("{0}-{1}", Environment.MachineName, name);

            InstanceProxy.IsFirstInstance = false;
            InstanceProxy.CommandLineArgs = Environment.GetCommandLineArgs();

            try {
                eventWaitHandle = EventWaitHandle.OpenExisting(eventName);
            } catch {
                InstanceProxy.IsFirstInstance = true;
            }

            if (InstanceProxy.IsFirstInstance) {
                eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, eventName);

                ThreadPool.RegisterWaitForSingleObject(eventWaitHandle, WaitOrTimerCallback, callback, Timeout.Infinite, false);
                eventWaitHandle.Close();

                RegisterRemoteType(name);
            } else {
                UpdateRemoteObject(name);

                if (eventWaitHandle != null)
                    eventWaitHandle.Set();

                Environment.Exit(0);
            }

            return InstanceProxy.IsFirstInstance;
        }

        private static void UpdateRemoteObject(string uri) {
            var clientChannel = new IpcClientChannel();
            ChannelServices.RegisterChannel(clientChannel, true);

            var proxy = Activator.GetObject(typeof (InstanceProxy), string.Format("ipc://{0}{1}/{1}", Environment.MachineName, uri)) as InstanceProxy;

            if (proxy != null)
                proxy.SetCommandLineArgs(InstanceProxy.IsFirstInstance, InstanceProxy.CommandLineArgs);

            ChannelServices.UnregisterChannel(clientChannel);
        }

        private static void RegisterRemoteType(string uri) {
            var serverChannel = new IpcServerChannel(Environment.MachineName + uri);
            ChannelServices.RegisterChannel(serverChannel, true);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof (InstanceProxy), uri, WellKnownObjectMode.Singleton);

            Process process = Process.GetCurrentProcess();
            process.Exited += delegate { ChannelServices.UnregisterChannel(serverChannel); };
        }

        private static void WaitOrTimerCallback(object state, bool timedOut) {
            var callback = state as EventHandler<InstanceCallbackEventArgs>;
            if (callback == null)
                return;

            callback(state, new InstanceCallbackEventArgs(InstanceProxy.IsFirstInstance, InstanceProxy.CommandLineArgs));
        }
    }
}