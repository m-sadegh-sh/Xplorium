namespace Xplorium.WSE {
    using System.Collections.Generic;

    public class NoCleaning : IBadWord {
        public bool BadWord(string word) {
            return true;
        }

        public IEnumerable<string> Clean(IEnumerable<string> words) {
            return words;
        }
    }
}