namespace Xplorium.Core {
    public abstract class LoggerBase {
        public string FileName { get; set; }
        public abstract void Append(string logMessage);
    }
}