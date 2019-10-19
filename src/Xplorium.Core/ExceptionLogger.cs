//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Globalization;
//using System.Runtime.InteropServices;
//using System.Runtime.Remoting.Messaging;
//using System.Text;
//using System.Threading;

//namespace Xplorium.Core.Utilities.Loggers
//{
//    public class ExceptionLogger
//    {
//        public ExceptionLogger()
//        {
//            Application.ThreadException += new ThreadExceptionEventHandler(OnThreadException);
//            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);
//            loggers = new List<Logger>();
//        }

//        private List<Logger> loggers;
//        public void AddLogger(Logger logger)
//        {
//            loggers.Add(logger);
//        }

//        public NotificationType NotificationType { get; set; }

//        delegate void LogExceptionDelegate(Exception exception);
//        private void HandleException(Exception exception)
//        {
//            switch (notificationType)
//            {
//                case NotificationType.Ask:
//                    if (MessageBox.Show("An unexpected error occurred - " + exception.Message + ". Do you wish to log the error?", "Error", MessageBoxButtons.YesNo) == DialogResult.No)
//                        return;
//                    break;
//                case NotificationType.Inform:
//                    MessageBox.Show("An unexpected error occurred - " + exception.Message);
//                    break;
//                case NotificationType.Silent:
//                    break;
//            }

//            LogExceptionDelegate logDelegate = new LogExceptionDelegate(LogException);
//            logDelegate.BeginInvoke(e, new AsyncCallback(LogCallBack), null);
//        }

//        private void OnThreadException(object sender, ThreadExceptionEventArgs e)
//        {
//            HandleException(e.Exception);
//        }

//        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
//        {
//            HandleException((Exception)e.ExceptionObject);
//        }

//        private void LogCallBack(IAsyncResult result)
//        {
//            AsyncResult asyncResult = (AsyncResult)result;
//            LogExceptionDelegate logDelegate = (LogExceptionDelegate)asyncResult.AsyncDelegate;
//            if (!asyncResult.EndInvokeCalled)
//                logDelegate.EndInvoke(result);
//        }

//        private string GetExceptionTypeStack(Exception exception)
//        {
//            if (exception.InnerException != null)
//            {
//                StringBuilder message = new StringBuilder();
//                message.AppendLine(GetExceptionTypeStack(exception.InnerException));
//                message.AppendLine("   " + exception.GetType().ToString());
//                return (message.ToString());
//            }
//            else
//                return "   " + exception.GetType().ToString();
//        }

//        private string GetExceptionMessageStack(Exception exception)
//        {
//            if (exception.InnerException != null)
//            {
//                StringBuilder message = new StringBuilder();
//                message.AppendLine(GetExceptionMessageStack(exception.InnerException));
//                message.AppendLine("   " + exception.Message);
//                return (message.ToString());
//            }
//            else
//                return "   " + exception.Message;
//        }

//        private string GetExceptionCallStack(Exception exception)
//        {
//            if (exception.InnerException != null)
//            {
//                StringBuilder message = new StringBuilder();
//                message.AppendLine(GetExceptionCallStack(exception.InnerException));
//                message.AppendLine("--- Next Call Stack:");
//                message.AppendLine(exception.StackTrace);
//                return (message.ToString());
//            }
//            else
//                return exception.StackTrace;
//        }

//        private static TimeSpan GetSystemUpTime()
//        {
//            PerformanceCounter upTime = new PerformanceCounter("System", "System Up Time");
//            upTime.NextValue();
//            return TimeSpan.FromSeconds(upTime.NextValue());
//        }

//        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
//        private class MEMORYSTATUSEX
//        {
//            public uint dwLength;
//            public uint dwMemoryLoad;
//            public ulong ullTotalPhys;
//            public ulong ullAvailPhys;
//            public ulong ullTotalPageFile;
//            public ulong ullAvailPageFile;
//            public ulong ullTotalVirtual;
//            public ulong ullAvailVirtual;
//            public ulong ullAvailExtendedVirtual;

//            public MEMORYSTATUSEX()
//            {
//                this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
//            }
//        }

//        [return: MarshalAs(UnmanagedType.Bool)]
//        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//        static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

//        public void LogException(Exception exception)
//        {
//            StringBuilder error = new StringBuilder();

//            error.AppendLine("Application:       " + Application.ProductName);
//            error.AppendLine("Version:           " + Application.ProductVersion);
//            error.AppendLine("Date:              " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
//            error.AppendLine("Computer name:     " + SystemInformation.ComputerName);
//            error.AppendLine("User name:         " + SystemInformation.UserName);
//            error.AppendLine("OS:                " + Environment.OSVersion.ToString());
//            error.AppendLine("Culture:           " + CultureInfo.CurrentCulture.Name);
//            error.AppendLine("Resolution:        " + SystemInformation.PrimaryMonitorSize.ToString());
//            error.AppendLine("System up time:    " + GetSystemUpTime());
//            error.AppendLine("App up time:       " +
//              (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString());

//            MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();
//            if (GlobalMemoryStatusEx(memStatus))
//            {
//                error.AppendLine("Total memory:      " + memStatus.ullTotalPhys / (1024 * 1024) + "Mb");
//                error.AppendLine("Available memory:  " + memStatus.ullAvailPhys / (1024 * 1024) + "Mb");
//            }

//            error.AppendLine("");

//            error.AppendLine("Exception classes:   ");
//            error.Append(GetExceptionTypeStack(exception));
//            error.AppendLine("");
//            error.AppendLine("Exception messages: ");
//            error.Append(GetExceptionMessageStack(exception));

//            error.AppendLine("");
//            error.AppendLine("Stack Traces:");
//            error.Append(GetExceptionCallStack(exception));
//            error.AppendLine("");
//            error.AppendLine("Loaded Modules:");
//            Process thisProcess = Process.GetCurrentProcess();
//            foreach (ProcessModule module in thisProcess.Modules)
//                error.AppendLine(module.FileName + " " + module.FileVersionInfo.FileVersion);

//            for (int i = 0; i < loggers.Count; i++)
//                loggers[i].LogError(error.ToString());
//        }
//    }
//}

