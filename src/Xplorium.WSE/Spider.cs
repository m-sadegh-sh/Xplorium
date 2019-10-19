namespace Xplorium.WSE {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Xplorium.Common;
    using Xplorium.Core;
    using Xplorium.Models;
    using Xplorium.Repositories;

    public class Spider {
        private UrlRepository _urlRepository;
        private CacheRepository _cacheRepository;
        private IList<Cache> _fetchedCaches;
        private Cache _currentCache;
        private WordRepository _wordRepository;
        private ParsedContentRepository _parsedContentRepository;
        private ParsedContent _currentParsedContent;
        private HitRepository _hitRepository;
        private IBadWord _iBadWord;
        private bool _isNew;
        private string _decompressedResponse;
        private bool _remainedAnyThing;

        public bool CanBeContinued { get; private set; }
        public Stopwatch Watcher { get; set; }
        public int TotalFetchedCaches { get; set; }
        public int MaximumFetchCaches { get; set; }
        public Option Option { get; set; }
        public int CurrentCacheIndex { get; set; }
        public string CurrentUrlPath { get; set; }
        public bool Locked { get; set; }
        public bool Unlocked { get; set; }

        private int _currentSubSectionCount;

        public int CurrentSubSectionCount {
            get { return _currentSubSectionCount; }
            set {
                _currentSubSectionCount = value;
                InvokeCounter();
            }
        }

        private int _currentSubSectionIndex;

        public int CurrentSubSectionIndex {
            get { return _currentSubSectionIndex; }
            set {
                _currentSubSectionIndex = value;
                InvokeCounter();
            }
        }

        public event LoggerEventHandler Logger;
        public event CounterEventHandler Counter;

        public Spider(LoggerEventHandler logger, CounterEventHandler counter, int maximumFetchCaches) {
            Option = new Option();
            Logger = logger;
            Counter = counter;
            MaximumFetchCaches = maximumFetchCaches;
            CanBeContinued = true;
            Initialize();
        }

        private void Initialize() {
            InvokeLogger("Initializing...", Option.StatusColors.Title);
            var succeeded = true;
            try {
                Watcher = new Stopwatch();
                Watcher.Start();

                switch (Preferences.BadWordCleaningMode) {
                    case BadWordMode.NoCleaning:
                        _iBadWord = new NoCleaning();
                        InvokeLogger("Word filtering mode: NoCleaning", Option.StatusColors.Default);
                        break;
                    case BadWordMode.ShortCleaning:
                        _iBadWord = new ShortCleaning();
                        InvokeLogger("Word filtering mode: ShortCleaning", Option.StatusColors.Default);
                        break;
                }
                _urlRepository = new UrlRepository();
                _cacheRepository = new CacheRepository();
                _wordRepository = new WordRepository();
                _parsedContentRepository = new ParsedContentRepository();
                _hitRepository = new HitRepository();
                InvokeLogger("Fetching parsable caches", Option.StatusColors.Default);
                _fetchedCaches = _cacheRepository.ParsableCaches(MaximumFetchCaches);
                TotalFetchedCaches = _fetchedCaches.Count;
                if (TotalFetchedCaches > 0) {
                    InvokeLogger(string.Format("{0} caches have been loaded", TotalFetchedCaches), Option.StatusColors.Default);
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
            InvokeLogger("Preparing cache object...", Option.StatusColors.Title);
            var succeeded = true;
            try {
                if (CurrentCacheIndex < TotalFetchedCaches) {
                    _currentCache = _fetchedCaches.ElementAt(CurrentCacheIndex);
                    CurrentUrlPath = _currentCache.Url.ResolvedPath;

                    InvokeLogger("Decompression response...", Option.StatusColors.Default);
                    _decompressedResponse = _currentCache.Response.Decompress();
                    _currentParsedContent = _parsedContentRepository.Get(_currentCache.CacheId);
                    if (_currentParsedContent == null) {
                        _currentParsedContent = new ParsedContent {
                            ParsedContentId = _currentCache.CacheId
                        };
                        _isNew = true;
                    }
                    _currentParsedContent.ParsedOn = DateTime.Now;
                    _currentParsedContent.CanBeFollowed = true;
                    _currentParsedContent.CanBeIndexed = true;
                    _remainedAnyThing = true;
                    CurrentCacheIndex++;
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
            if (Logger != null && !string.IsNullOrWhiteSpace(message))
                Logger(message, foreColor);
        }

        private void InvokeCounter() {
            if (CurrentSubSectionCount > 0 && CurrentSubSectionIndex >= 0 && CurrentSubSectionIndex <= CurrentSubSectionCount)
                Counter();
        }

        public bool Next() {
            var succeeded = true;
            try {
                if (_remainedAnyThing) {
                    Parse();
                    Follow();
                    if (!_currentParsedContent.CanBeFollowed)
                        InvokeLogger("This document preventing us from following urls.", Option.StatusColors.Default);
                    Index();
                    if (!_currentParsedContent.CanBeIndexed)
                        InvokeLogger("This document preventing us from indexing contents.", Option.StatusColors.Default);
                } else {
                    InvokeLogger("Can't be fetching cache object.", Option.StatusColors.Failed);
                    succeeded = false;
                }
            } catch (Exception exception) {
                InvokeLogger(exception.Message, Option.StatusColors.Failed);
                succeeded = false;
            }
            if (succeeded) {
                _remainedAnyThing = false;
                Prepare();
            }
            return succeeded;
        }

        public void Lock() {
            InvokeLogger("Locking...", Option.StatusColors.Title);
            var succeeded = true;
            try {
                _cacheRepository.Lock(_fetchedCaches.Select(cache => cache.CacheId).ToList());
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
                _cacheRepository.Unlock(_fetchedCaches.Select(cache => cache.CacheId).ToList());
                Unlocked = true;
            } catch (Exception exception) {
                InvokeLogger(exception.Message, Option.StatusColors.Failed);
                succeeded = false;
            } finally {
                InvokeLogger("Unlocking have been " + (succeeded ? "succeeded" : "failed"), (succeeded ? Option.StatusColors.Succeeded : Option.StatusColors.Failed));
            }
        }

        private void Parse() {
            InvokeLogger("Parsing...", Option.StatusColors.Title);
            var succeeded = true;
            try {
                _currentParsedContent.Title = Regex.Match(_decompressedResponse, PreparedExpressions.TitleTag, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture).Value.Substring(0, 71, false);
                foreach (Match metaMatch in
                    Regex.Matches(_decompressedResponse, PreparedExpressions.MetaTags, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture)) {
                    var metaKey = string.Empty;
                    var metaValue = string.Empty;
                    foreach (Match metaValueMatch in
                        Regex.Matches(metaMatch.Value, PreparedExpressions.MetaTagAttributes, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture)) {
                        var metaKeyLower = metaValueMatch.Groups[1].ToString().ToLower();
                        var metaValueLower = metaValueMatch.Groups[2].ToString();
                        if (metaKeyLower == "http-equiv")
                            metaKey = metaValueLower;
                        if ((metaKeyLower == "name") && (metaKey == string.Empty))
                            metaKey = metaValueLower;
                        if (metaKeyLower == "content")
                            metaValue = metaValueLower;
                    }
                    switch (metaKey.ToLower()) {
                        case "description":
                            _currentParsedContent.Description = metaValue.Substring(0, 255, false);
                            break;
                        case "keywords":
                        case "keyword":
                            _currentParsedContent.Keywords = metaValue.Substring(0, 179, false);
                            break;
                        case "robots":
                        case "robot":
                            DetectPermissions(metaValue);
                            break;
                    }
                }
            } catch (Exception exception) {
                InvokeLogger(exception.Message, Option.StatusColors.Failed);
                succeeded = false;
            } finally {
                InvokeLogger("Parsing have been " + (succeeded ? "succeeded" : "failed"), (succeeded ? Option.StatusColors.Succeeded : Option.StatusColors.Failed));
            }
        }

        private void Follow() {
            InvokeLogger("Following... (duplicated urls will be ignored)", Option.StatusColors.Title);
            var succeeded = true;
            try {
                var matches = Regex.Matches(_decompressedResponse, PreparedExpressions.ATags, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                CurrentSubSectionCount = matches.Count;
                CurrentSubSectionIndex = 0;
                foreach (Match match in matches) {
                    var _succeeded = false;
                    var detected = string.Empty;
                    foreach (var subMatch in
                        Regex.Matches(match.Value, PreparedExpressions.ATagAttributes, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture).Cast<Match>().Where(subMatch => subMatch.Groups[1].ToString().ToLower() == "href")) {
                        detected = subMatch.Groups[2].ToString();
                        break;
                    }

                    detected = StringExtensions.ResolveUrl(detected, new Uri(_currentCache.Url.DetectedPath));

                    if (!StringExtensions.UrlIsValid(detected)) {
                        InvokeLogger("Url skipped because length is more-than 2048 characters", Option.StatusColors.Failed);
                        continue;
                    }

                    var newUrl = _urlRepository.Get(detected);
                    if (newUrl == null) {
                        newUrl = new Url {
                            UrlId = Guid.NewGuid(),
                            FollowedOn = DateTime.Now,
                            Approved = true,
                            DetectedPath = detected,
                            ResolvedPath = detected,
                            Rate = 1.0D
                        };

                        if (_urlRepository.Create(newUrl))
                            _succeeded = true;
                        InvokeLogger(newUrl.ResolvedPath, (_succeeded ? Option.StatusColors.Succeeded : Option.StatusColors.Failed));
                    }
                    CurrentSubSectionIndex++;
                }
            } catch (Exception exception) {
                InvokeLogger(exception.Message, Option.StatusColors.Failed);
                succeeded = false;
            } finally {
                InvokeLogger(string.Format("Following have been {0}", (succeeded ? "succeeded" : "failed")), (succeeded ? Option.StatusColors.Succeeded : Option.StatusColors.Failed));
            }
        }

        private void Index() {
            InvokeLogger("Indexing... (count of duplicated words will be increased)", Option.StatusColors.Title);
            var succeeded = true;
            try {
                var cleanContent = StringExtensions.StripHtml(_decompressedResponse);

                if (string.IsNullOrWhiteSpace(_currentParsedContent.Title))
                    _currentParsedContent.Title = cleanContent.Substring(0, 71, false);

                if (string.IsNullOrWhiteSpace(_currentParsedContent.Description))
                    _currentParsedContent.Description = cleanContent.Substring(0, 255, false);

                var documentIndexed = _isNew ? _parsedContentRepository.Create(_currentParsedContent) : _parsedContentRepository.SubmitChanges();

                InvokeLogger(_currentParsedContent.Title ?? _currentParsedContent.ParsedContentId.ToString(), (documentIndexed ? Option.StatusColors.Succeeded : Option.StatusColors.Failed));

                InvokeLogger("Filtering noise words of stripped content", Option.StatusColors.Default);
                var wordsList = _iBadWord.Clean(cleanContent.Split(' '));

                var wordIndexed = false;
                CurrentSubSectionCount = wordsList.Count();
                CurrentSubSectionIndex = 0;
                foreach (var text in wordsList) {
                    var newWord = _wordRepository.Get(text);
                    if (newWord == null) {
                        newWord = new Word {
                            WordId = Guid.NewGuid(),
                            Text = text.Substring(0, 63, false)
                        };
                        wordIndexed = _wordRepository.Create(newWord);
                        InvokeLogger(newWord.Text, wordIndexed ? Option.StatusColors.Succeeded : Option.StatusColors.Failed);
                    }

                    if (documentIndexed && wordIndexed) {
                        var newHit = _hitRepository.Get(newWord.WordId, _currentParsedContent.ParsedContentId);
                        if (newHit == null) {
                            newHit = new Hit {
                                ParsedContentId = _currentParsedContent.ParsedContentId,
                                WordId = newWord.WordId,
                                Count = 1
                            };
                            _hitRepository.Create(newHit);
                        }
                    }
                    CurrentSubSectionIndex++;
                }
            } catch (Exception exception) {
                InvokeLogger(exception.Message, Option.StatusColors.Failed);
                succeeded = false;
            } finally {
                InvokeLogger("Indexing have been " + (succeeded ? "succeeded" : "failed"), (succeeded ? Option.StatusColors.Succeeded : Option.StatusColors.Failed));
            }
        }

        private void DetectPermissions(string robotMetaContent) {
            InvokeLogger("Detecting robots permissions...", Option.StatusColors.Title);
            robotMetaContent = robotMetaContent.ToLower();
            if (robotMetaContent.IndexOf("none") >= 0) {
                _currentParsedContent.CanBeFollowed = false;
                _currentParsedContent.CanBeIndexed = false;
            } else {
                if (robotMetaContent.IndexOf("noindex") > -1)
                    _currentParsedContent.CanBeIndexed = false;
                if (robotMetaContent.IndexOf("nofollow") > -1)
                    _currentParsedContent.CanBeFollowed = false;
            }
        }
    }
}