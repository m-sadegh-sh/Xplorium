namespace Xplorium.WpfSpider {
    using System;
    using System.Windows;

    public partial class App : Application {
        public App() {
            StartupUri = new Uri("TaskSelectorWindow.xaml", UriKind.Relative);
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        public static MessageBoxResult ShowWindow(Window owner, string message, string caption, MessageBoxButton buttons, MessageBoxImage image) {
            var messageWindow = new MessageWindow(owner, message, caption, buttons, image);
            messageWindow.ShowDialog();
            var widnowResult = messageWindow.MessageWindowResult;

            return widnowResult;
        }
    }
}