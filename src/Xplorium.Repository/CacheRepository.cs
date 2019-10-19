namespace Xplorium.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Xplorium.Models;

    public class CacheRepository : RepositoryBase {
        public int Count() {
            return DataContext.Caches.Count();
        }

        public Cache Get(Guid cacheId) {
            try {
                return DataContext.Caches.FirstOrDefault(q => q.CacheId == cacheId);
            } catch {
                return null;
            }
        }

        public IList<Cache> ParsableCaches(int count) {
            try {
                return DataContext.Caches.Where(cache => !cache.Locked && cache.ParsedContent == null).Take(count).ToList();
            } catch {
                return null;
            }
        }

        public void Lock(IList<Guid> cacheIds) {
            try {
                foreach (var cache in cacheIds.Select(Get)) {
                    cache.Locked = true;
                    SubmitChanges();
                }
            } catch {}
        }

        public void Unlock(IList<Guid> cacheIds) {
            try {
                foreach (var cache in cacheIds.Select(Get)) {
                    cache.Locked = false;
                    SubmitChanges();
                }
            } catch {}
        }

        public bool Create(Cache newCache, bool autoSave = true) {
            try {
                DataContext.Caches.InsertOnSubmit(newCache);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool Remove(Guid cacheId, bool autoSave = true) {
            try {
                var cache = Get(cacheId);
                DataContext.Caches.DeleteOnSubmit(cache);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }
    }
}