namespace Xplorium.Repositories {
    using System;
    using System.Linq;

    using Xplorium.Models;

    public class HitRepository : RepositoryBase {
        public int Count() {
            return DataContext.Hits.Count();
        }

        public Hit Get(Guid wordId, Guid parsedContentId) {
            try {
                return DataContext.Hits.FirstOrDefault(q => q.WordId == wordId && q.ParsedContentId == parsedContentId);
            } catch {
                return null;
            }
        }

        public bool IncreaseCount(Guid wordId, Guid parsedContentId, bool autoSave = true) {
            try {
                var hit = Get(wordId, parsedContentId);
                hit.Count++;
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool DecreaseCount(Guid wordId, Guid parsedContentId, bool autoSave = true) {
            try {
                var hit = Get(wordId, parsedContentId);
                hit.Count--;
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool Create(Hit newHit, bool autoSave = true) {
            try {
                DataContext.Hits.InsertOnSubmit(newHit);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool Modify(Hit modifiedHit, bool autoSave = true) {
            try {
                var originalHit = Get(modifiedHit.WordId, modifiedHit.ParsedContentId);
                //_repositoryDataContext.Hits.Attach(modifiedHit, originalHit);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool Remove(Guid wordId, Guid parsedContentId, bool autoSave = true) {
            try {
                var hit = Get(wordId, parsedContentId);
                DataContext.Hits.DeleteOnSubmit(hit);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }
    }
}