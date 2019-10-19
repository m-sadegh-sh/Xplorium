namespace Xplorium.WpfSpider {
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Forms;

    using Application = System.Windows.Application;

    public partial class TaskSelectorWindow : Window {
        public NotifyIcon Notifier { get; set; }

        public TaskSelectorWindow() {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void TaskSelectorWindow_Closing(object sender, CancelEventArgs e) {
            var windowCollection = Application.Current.Windows;
            var count = GetCount(windowCollection);
            if (count <= 0)
                return;
            var closed = false;
            var messageBoxResult = App.ShowWindow(this, string.Format("About {0} window are open still. Close the program may cause tasks to remain un-completed.\n\nAre you sure you want to close {1}?", count, Title), Title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes) {
                foreach (var window in windowCollection) {
                    var type = window.GetType();
                    if (type == typeof (SpiderWindow)) {
                        var spider = (SpiderWindow) window;
                        spider.Close();
                        closed = spider.Closedd;
                    } else if (type == typeof (CacherWindow)) {
                        var cacher = (CacherWindow) window;
                        cacher.Close();
                        closed = cacher.Closedd;
                    }
                    if (!closed && (type == typeof (SpiderWindow) || type == typeof (CacherWindow)))
                        break;
                }
                if (closed)
                    e.Cancel = false;
                else {
                    App.ShowWindow(this, "Closing have been cancelled because one or more of opened widows are still opens and/or can't close untill completing current task.", Title, MessageBoxButton.OK, MessageBoxImage.Information);
                    e.Cancel = true;
                }
            } else
                e.Cancel = true;
        }

        private static int GetCount(IEnumerable windowCollection) {
            return (from object window in windowCollection
                    select window.GetType()).Count(type => type == typeof (CacherWindow) || type == typeof (SpiderWindow));
        }

        public bool Activate(bool restoreIfMinimized) {
            if (restoreIfMinimized && WindowState == WindowState.Minimized)
                WindowState = WindowState.Normal;

            return Activate();
        }

        private void btnSpider_Click(object sender, RoutedEventArgs e) {
            new SpiderWindow().Show();
        }

        private void btnCacher_Click(object sender, RoutedEventArgs e) {
            new CacherWindow().Show();
        }

        private void TaskSelectorWindow_Loaded(object sender, RoutedEventArgs e) {
            Notifier = new NotifyIcon {
                Icon = Properties.Resources.Icon,
                ContextMenu = new ContextMenu()
            };
            Notifier.ContextMenu.MenuItems.AddRange(new[] {new MenuItem("&Hello")});
        }
    }
}