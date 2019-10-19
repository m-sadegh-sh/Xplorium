namespace Xplorium.WSE {
    public class Option {
        public StatusColors StatusColors { get; set; }

        public Option() : this(new StatusColors()) {}

        public Option(StatusColors statusColors) {
            StatusColors = statusColors;
        }
    }
}