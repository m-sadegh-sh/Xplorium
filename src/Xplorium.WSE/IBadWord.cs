namespace Xplorium.WSE {
    using System.Collections.Generic;

    public interface IBadWord {
        bool BadWord(string word);
        IEnumerable<string> Clean(IEnumerable<string> words);
    }
}