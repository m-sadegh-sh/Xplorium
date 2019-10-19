namespace Xplorium.Models {
    using System.Collections.Generic;

    using Xplorium.Common;

    public class Message {
        public MessageType MessageType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<string> Suggestions { get; set; }
    }
}