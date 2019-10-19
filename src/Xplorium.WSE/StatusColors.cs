namespace Xplorium.WSE {
    using System.Drawing;

    public class StatusColors {
        public Color Default { get; set; }
        public Color Title { get; set; }
        public Color Succeeded { get; set; }
        public Color Failed { get; set; }
        public Color Warning { get; set; }

        public StatusColors() {
            Default = Color.FromKnownColor(KnownColor.Gray);
            Title = Color.FromKnownColor(KnownColor.RoyalBlue);
            Succeeded = Color.FromKnownColor(KnownColor.Green);
            Failed = Color.FromKnownColor(KnownColor.Firebrick);
            Warning = Color.FromKnownColor(KnownColor.DarkOrange);
        }
    }
}