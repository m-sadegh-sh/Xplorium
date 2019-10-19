namespace Xplorium.WpfSpider {
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Media;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;

    public partial class MessageWindow : Window {
        public MessageBoxResult MessageWindowResult { get; private set; }

        public MessageWindow(Window owner, string message, string caption, MessageBoxButton buttons, MessageBoxImage image) {
            InitializeComponent();
            var assembly = Assembly.GetEntryAssembly().GetName();
            Title = assembly.Name;
            Owner = owner;
            tbMessage.Text = message;
            tbCaption.Text = caption;
            switch (buttons) {
                case MessageBoxButton.OKCancel:
                    btnOK.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.YesNo:
                    btnYes.Visibility = Visibility.Visible;
                    btnNo.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.YesNoCancel:
                    btnYes.Visibility = Visibility.Visible;
                    btnNo.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.OK:
                default:
                    btnOK.Visibility = Visibility.Visible;
                    break;
            }
            Icon icon = null;
            switch (image) {
                case MessageBoxImage.Asterisk:
                    icon = SystemIcons.Asterisk;
                    break;
                case MessageBoxImage.Error:
                    icon = SystemIcons.Error;
                    break;
                case MessageBoxImage.Exclamation:
                    icon = SystemIcons.Exclamation;
                    break;
                case MessageBoxImage.Question:
                    icon = SystemIcons.Question;
                    break;
            }
            if (icon != null) {
                var bitmap = icon.ToBitmap();
                iImage.Source = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                iImage.Visibility = Visibility.Visible;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            MessageWindowResult = MessageBoxResult.Cancel;
            Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e) {
            MessageWindowResult = MessageBoxResult.No;
            Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e) {
            MessageWindowResult = MessageBoxResult.Yes;
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            MessageWindowResult = MessageBoxResult.OK;
            Close();
        }

        private void MessageWindow_Loaded(object sender, RoutedEventArgs e) {
            SystemSounds.Asterisk.Play();
        }

        private void MessageWindow_Closing(object sender, CancelEventArgs e) {
            e.Cancel = MessageWindowResult == MessageBoxResult.None;
        }
    }
}