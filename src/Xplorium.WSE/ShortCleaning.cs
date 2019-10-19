namespace Xplorium.WSE {
    using System.Collections.Generic;
    using System.Linq;

    public class ShortCleaning : IBadWord {
        public bool BadWord(string word) {
            return (word.Length >= 3);
        }

        public IEnumerable<string> Clean(IEnumerable<string> words) {
            return words.Where(BadWord);
        }
    }
}