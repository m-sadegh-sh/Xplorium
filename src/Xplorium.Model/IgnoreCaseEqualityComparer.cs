namespace Xplorium.Models {
    using System.Collections.Generic;

    public class IgnoreCaseEqualityComparer : IEqualityComparer<string> {
        public bool Equals(string x, string y) {
            return (x.ToLower() == y.ToLower());
        }

        public int GetHashCode(string obj) {
            return obj.GetHashCode();
        }
    }
}