namespace Xplorium.WpfSpider {
    using System.Windows;
    using System.Windows.Controls;

    public partial class PenddingLayout : UserControl {
        public string Message {
            set { tbMessage.Text = value; }
        }

        public PenddingLayout() {
            InitializeComponent();
        }

        public void Visible(string message) {
            Visibility = Visibility.Visible;
            Message = message;
        }

        public void Hidden() {
            Visibility = Visibility.Hidden;
        }
    }
}