namespace Xplorium.Windows.Api {
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;

    using Brushes = System.Windows.Media.Brushes;

    public static class GlassExtender {
        [StructLayout(LayoutKind.Sequential)]
        private struct MARGINS {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        private static extern int DwmIsCompositionEnabled(ref int en);

        public static void ExtendGlass(Window window, Thickness thikness) {
            try {
                int isGlassEnabled = 0;
                DwmIsCompositionEnabled(ref isGlassEnabled);
                if (Environment.OSVersion.Version.Major > 5 && isGlassEnabled > 0) {
                    var helper = new WindowInteropHelper(window);
                    var mainWindowSrc = HwndSource.FromHwnd(helper.Handle);
                    mainWindowSrc.CompositionTarget.BackgroundColor = Colors.Transparent;

                    var desktop = Graphics.FromHwnd(mainWindowSrc.Handle);
                    float dpiX = desktop.DpiX/96;
                    float dpiY = desktop.DpiY/96;

                    var margins = new MARGINS();
                    margins.cxLeftWidth = (int) (thikness.Left*dpiX);
                    margins.cxRightWidth = (int) (thikness.Right*dpiX);
                    margins.cyBottomHeight = (int) (thikness.Bottom*dpiY);
                    margins.cyTopHeight = (int) (thikness.Top*dpiY);

                    window.Background = Brushes.Transparent;

                    int hr = DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);
                } else
                    window.Background = Brushes.White;
            } catch (DllNotFoundException exception) {
                MessageBox.Show(string.Format("We're sorry because extending glass can't be completed.\n\n{0}", exception.Message), "HH", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}