namespace Xplorium.Core {
    using System;
    using System.Linq.Expressions;

    public static class CheckArgument {
        public static bool IsNull<T>(Expression<Func<T>> expression) where T : class {
            var compiled = expression.Compile();
            return (compiled() == null);
        }
    }
}