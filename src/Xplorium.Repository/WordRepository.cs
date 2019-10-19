namespace Xplorium.Repositories {
    using System;
    using System.Linq;

    using Xplorium.Models;

    public class WordRepository : RepositoryBase {
        public int Count() {
            return DataContext.Words.Count();
        }

        public Word Get(Guid wordId) {
            try {
                return DataContext.Words.FirstOrDefault(q => q.WordId == wordId);
            } catch {
                return null;
            }
        }

        public Word Get(string text) {
            try {
                return DataContext.Words.FirstOrDefault(q => q.Text == text);
            } catch {
                return null;
            }
        }

        public bool Create(Word newWord, bool autoSave = true) {
            try {
                DataContext.Words.InsertOnSubmit(newWord);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool Modify(Word modifiedWord, Guid wordId, bool autoSave = true) {
            try {
                var originalWord = Get(wordId);
                //_repositoryDataContext.Words.Attach(modifiedWord, originalWord);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool Remove(Guid wordId, bool autoSave = true) {
            try {
                var word = Get(wordId);
                DataContext.Words.DeleteOnSubmit(word);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }
    }
}