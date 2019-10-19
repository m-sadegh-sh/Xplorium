namespace Xplorium.Repositories {
    using System;
    using System.Linq;

    using Xplorium.Models;

    public class ParsedContentRepository : RepositoryBase {
        public int Count() {
            return DataContext.ParsedContents.Count();
        }

        public ParsedContent Get(Guid parsedContentId) {
            try {
                return DataContext.ParsedContents.FirstOrDefault(q => q.ParsedContentId == parsedContentId);
            } catch {
                return null;
            }
        }

        public bool Create(ParsedContent newParsedContent, bool autoSave = true) {
            try {
                DataContext.ParsedContents.InsertOnSubmit(newParsedContent);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool Remove(Guid parsedContentId, bool autoSave = true) {
            try {
                var parsedContent = Get(parsedContentId);
                DataContext.ParsedContents.DeleteOnSubmit(parsedContent);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }
    }
}