namespace Xplorium.Repositories {
    using System;

    using Xplorium.Models;

    public abstract class RepositoryBase {
        private readonly XploriumDataContext _dataContext;

        public XploriumDataContext DataContext {
            get { return _dataContext; }
        }

        protected RepositoryBase() {
            _dataContext = new XploriumDataContext {
                Log = Console.Out
            };
        }

        public bool SubmitChanges() {
            try {
                DataContext.SubmitChanges();
                return true;
            } catch {
                return false;
            }
        }
    }
}