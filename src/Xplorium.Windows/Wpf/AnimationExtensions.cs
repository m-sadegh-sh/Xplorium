namespace Xplorium.Windows.Wpf {
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public static class AnimationExtensions {
        public static void ToggleFade(this Control control) {
            ToggleFade(control, control.Opacity <= 0.5 ? 0.0 : 1.0, control.Opacity > 0.5 ? 1.0 : 0.0, TransitionSpeed.Normal);
        }

        public static void FadeIn(this Control control) {
            control.FadeIn(TransitionSpeed.Normal);
        }

        public static void FadeIn(this Control control, TransitionSpeed speed) {
            ToggleFade(control, control.Opacity, 1.0, speed);
        }

        public static void FadeOut(this Control control) {
            control.FadeOut(TransitionSpeed.Normal);
        }

        public static void FadeOut(this Control control, TransitionSpeed speed) {
            ToggleFade(control, control.Opacity, 0.0, speed);
        }

        private static void ToggleFade(Control control, double from, double to, TransitionSpeed speed) {
            var storyboard = new Storyboard();
            var duration = new TimeSpan(0, 0, 0, 0, (int) speed);

            var animation = new DoubleAnimation {
                From = from,
                To = to,
                Duration = new Duration(duration)
            };

            Storyboard.SetTargetName(animation, control.Name);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity", 0));
            storyboard.Children.Add(animation);

            storyboard.Begin(control);
        }
    }
}