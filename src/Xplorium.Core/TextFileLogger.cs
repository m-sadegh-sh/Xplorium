namespace Xplorium.Core {
    using System;
    using System.IO;

    public class TextFileLogger : LoggerBase {
        public TextFileLogger(string fileName) {
            FileName = fileName;
        }

        public override void Append(string logMessage) {
            try {
                string formatedMessage = string.Format("{0} - {1}", DateTime.Now, logMessage.Trim());
                using (var stream = new StreamWriter(FileName, true))
                    stream.WriteLine(formatedMessage);
            } catch {}
        }
    }
}