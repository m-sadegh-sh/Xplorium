namespace Xplorium.WSE {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    using Xplorium.Core;
    using Xplorium.Models;
    using Xplorium.Repositories;

    public class Cacher {
        private UrlRepository _urlRepository;
        private IList<Url> _fetchedUrls;
        private Url _currentUrl;
        private CacheRepository _cacheRepository;
        private Cache _currentCache;
        private bool _isNew;
        private bool _remainedAnyThing;

        public Stopwatch Watcher { get; set; }
        public long TransferredBytes { get; set; }
        public int TotalFetchedUrls { get; set; }
        public int MaximumFetchedUrls { get; set; }
        public int CurrentUrlIndex { get; set; }
        public string CurrentUrlPath { get; set; }
        public bool CanBeContinued { get; private set; }
        public event LoggerEventHandler Logger;
        public Option Option { get; set; }
        public bool Locked { get; set; }
        public bool Unlocked { get; set; }

        public Cacher(LoggerEventHandler logger, int maximumFetchedUrls) {
            Option = new Option();
            Logger = logger;
            MaximumFetchedUrls = maximumFetchedUrls;
            CanBeContinued = true;
            Initialize();
        }

        private void Initialize() {
            InvokeLogger("Initializing...", Option.StatusColors.Title);
            var succeeded = true;
            try {
                Watcher = new Stopwatch();
                Watcher.Start();
                _urlRepository = new UrlRepository();
                _cacheRepository = new CacheRepository();
                InvokeLogger("Fetching cacheable urls...", Option.StatusColors.Default);
                _fetchedUrls = _urlRepository.CacheableUrls(MaximumFetchedUrls);
                TotalFetchedUrls = _fetchedUrls.Count();
                CurrentUrlIndex = 0;
                TransferredBytes = 0L;
                if (TotalFetchedUrls > 0) {
                    InvokeLogger(string.Format("{0} urls have been loaded", TotalFetchedUrls), Option.StatusColors.Default);
                    Lock();
                    Prepare();
                }
            } catch (Exception exception) {
                InvokeLogger(exception.Message, Option.StatusColors.Failed);
                succeeded = false;
            } finally {
                InvokeLogger("Initializing have been " + (succeeded ? "succeeded" : "failed"), (succeeded ? Option.StatusColors.Succeeded : Option.StatusColors.Failed));
            }
        }

        private void Prepare() {
            InvokeLogger("Preparing url object...", Option.StatusColors.Title);
            var succeeded = true;
            try {
                if (CurrentUrlIndex < TotalFetchedUrls) {
                    _currentUrl = _fetchedUrls.ElementAt(CurrentUrlIndex);
                    CurrentUrlPath = _currentUrl.ResolvedPath;
                    _currentCache = _cacheRepository.Get(_currentUrl.UrlId);
                    if (_currentCache == null) {
                        _currentCache = new Cache {
                            CacheId = _currentUrl.UrlId
                        };
                        _isNew = true;
                    }
                    _currentCache.CachedOn = DateTime.Now;
                    _remainedAnyThing = true;
                }
            } catch (Exception exception) {
                InvokeLogger(exception.Message, Option.StatusColors.Failed);
                CanBeContinued = _remainedAnyThing = false;
                Unlock();
                Watcher.Stop();
                succeeded = false;
            } finally {
                InvokeLogger("Preparing have been " + (succeeded ? "succeeded" : "failed"), (succeeded ? Option.StatusColors.Succeeded : Option.StatusColors.Failed));
            }
        }

        private void InvokeLogger(string message, Color foreColor) {
            if (Logger != null && !string.IsNullOrEmpty(message))
                Logger(message, foreColor);
        }

        public bool Next() {
            var succeeded = true;
            try {
                if (_remainedAnyThing) {
                    var currentUrl = _currentUrl.ResolvedPath;
                    InvokeLogger(currentUrl, Option.StatusColors.Default);
                    if (Download()) {
                        if (_isNew)
                            _cacheRepository.Create(_currentCache);
                        InvokeLogger("Content have been cached", Option.StatusColors.Succeeded);

                        InvokeLogger("Checking response final url", Option.StatusColors.Default);
                        if (_currentUrl.ResolvedPath != currentUrl) {
                            _urlRepository.SubmitChanges();
                            InvokeLogger(string.Format("Url have been resolved to {0}", _currentUrl.ResolvedPath), Option.StatusColors.Succeeded);
                        }
                    } else {
                        _currentUrl.Rate -= 0.1D;
                        _urlRepository.SubmitChanges();
                        InvokeLogger("Rate of url decreased.", Option.StatusColors.Warning);
                        succeeded = false;
                    }
                } else {
                    InvokeLogger("Can't be fetching url object.", Option.StatusColors.Failed);
                    succeeded = false;
                }
            } catch (Exception exception) {
                InvokeLogger(exception.Message, Option.StatusColors.Failed);
                succeeded = false;
            }
            if (succeeded)
                _remainedAnyThing = false;
            CurrentUrlIndex++;
            Prepare();
            return succeeded;
        }

        public void Lock() {
            InvokeLogger("Locking...", Option.StatusColors.Title);
            var succeeded = true;
            try {
                _urlRepository.Lock(_fetchedUrls.Select(url => url.UrlId).ToList());
                Locked = true;
            } catch (Exception exception) {
                InvokeLogger(exception.Message, Option.StatusColors.Failed);
                succeeded = false;
            } finally {
                InvokeLogger("Locking have been " + (succeeded ? "succeeded" : "failed"), (succeeded ? Option.StatusColors.Succeeded : Option.StatusColors.Failed));
            }
        }

        public void Unlock() {
            InvokeLogger("Unlocking...", Option.StatusColors.Title);
            var succeeded = true;
            try {
                _urlRepository.Unlock(_fetchedUrls.Select(url => url.UrlId).ToList());
                Unlocked = true;
            } catch (Exception exception) {
                InvokeLogger(exception.Message, Option.StatusColors.Failed);
                succeeded = false;
            } finally {
                InvokeLogger("Unlocking have been " + (succeeded ? "succeeded" : "failed"), (succeeded ? Option.StatusColors.Succeeded : Option.StatusColors.Failed));
            }
        }

        private bool Download() {
            InvokeLogger("Downloading...", Option.StatusColors.Title);
            var succeeded = true;
            try {
                using (var webResponse = GetResponse(_currentUrl.ResolvedPath)) {
                    if (webResponse != null && webResponse.StatusCode == HttpStatusCode.OK) {
                        var mimeType = webResponse.ContentType.Split(';')[0];
                        _currentUrl.ResolvedPath = webResponse.ResponseUri.AbsoluteUri;

                        InvokeLogger("Checking response mime-type", Option.StatusColors.Default);
                        switch (mimeType.ToLower()) {
                            case "text/html":
                            case "text/htm":
                                InvokeLogger("Reading response stream", Option.StatusColors.Default);
                                using (var streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8)) {
                                    var response = streamReader.ReadToEnd();
                                    var length = response.Length;
                                    if (length < 128) {
                                        InvokeLogger("Response length is lower-than 128", Option.StatusColors.Warning);
                                        succeeded = false;
                                    } else {
                                        InvokeLogger("Compression response", Option.StatusColors.Default);
                                        _currentCache.Response = response.Compress();
                                        _currentCache.Length = length;
                                    }
                                    TransferredBytes += length;
                                }
                                break;
                            default:
                                InvokeLogger("Invalid mime-type", Option.StatusColors.Warning);
                                succeeded = false;
                                break;
                        }
                    } else
                        succeeded = false;
                }
            } catch (Exception exception) {
                InvokeLogger(exception.Message, Option.StatusColors.Failed);
                succeeded = false;
            } finally {
                InvokeLogger("Dowloading have been " + (succeeded ? "succeeded" : "failed"), (succeeded ? Option.StatusColors.Succeeded : Option.StatusColors.Failed));
            }
            return succeeded;
        }

        private static HttpWebResponse GetResponse(string absolutePath) {
            var httpWebRequest = WebRequest.Create(absolutePath) as HttpWebRequest;
            if (httpWebRequest != null) {
                httpWebRequest.AllowAutoRedirect = true;
                httpWebRequest.MaximumAutomaticRedirections = 3;
                httpWebRequest.UserAgent = Preferences.UserAgent;
                httpWebRequest.KeepAlive = true;
                httpWebRequest.Timeout = Preferences.RequestTimeout*1000;
                return httpWebRequest.GetResponse() as HttpWebResponse;
            }
            return null;
        }
    }
}