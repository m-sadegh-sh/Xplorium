namespace Xplorium.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Xplorium.Models;

    public class UrlRepository : RepositoryBase {
        public int Count() {
            return DataContext.Urls.Count();
        }

        public Url Get(string path) {
            try {
                return DataContext.Urls.FirstOrDefault(q => q.DetectedPath == path || q.ResolvedPath == path);
            } catch {
                return null;
            }
        }

        public Url Get(Guid urlId) {
            try {
                return DataContext.Urls.FirstOrDefault(q => q.UrlId == urlId);
            } catch {
                return null;
            }
        }

        public IList<Url> CacheableUrls(int count) {
            try {
                return DataContext.Urls.Where(url => url.Approved && !url.Locked && url.ResolvedPath.Contains("microsoft.com/") && url.Cache == null).Take(count).OrderByDescending(url => url.Rate).ToList();
            } catch {
                return null;
            }
        }

        public void Lock(IList<Guid> urlIds) {
            try {
                foreach (var url in urlIds.Select(Get)) {
                    url.Locked = true;
                    SubmitChanges();
                }
            } catch {}
        }

        public void Unlock(IList<Guid> urlIds) {
            try {
                foreach (var url in urlIds.Select(Get)) {
                    url.Locked = false;
                    SubmitChanges();
                }
            } catch {}
        }

        public bool Create(Url newUrl, bool autoSave = true) {
            try {
                DataContext.Urls.InsertOnSubmit(newUrl);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool Modify(Url modifiedUrl, bool autoSave = true) {
            try {
                var url = Get(modifiedUrl.UrlId);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool Remove(Guid urlId, bool autoSave = true) {
            try {
                var url = Get(urlId);
                DataContext.Urls.DeleteOnSubmit(url);
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool Approve(Guid urlId, bool state, bool autoSave = true) {
            try {
                var url = Get(urlId);
                url.Approved = state;
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool RateTo(Guid urlId, double changeAmount, bool autoSave = true) {
            try {
                var url = Get(urlId);
                url.Rate += changeAmount;
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }

        public bool ResolveUrl(Guid urlId, string resolvedPath, bool autoSave = true) {
            try {
                var url = Get(urlId);
                url.ResolvedPath = resolvedPath;
                return autoSave ? SubmitChanges() : true;
            } catch {
                return false;
            }
        }
    }
}