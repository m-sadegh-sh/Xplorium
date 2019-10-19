namespace Xplorium.Web.Mvc {
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Routing;

    internal class PagerBuilder {
        private readonly HtmlHelper _html;
        private readonly AjaxHelper _ajax;
        private readonly string _actionName;
        private readonly string _controllerName;
        private readonly int _totalPageCount = 1;
        private readonly int _pageIndex;
        private readonly PagerOptions _pagerOptions;
        private readonly RouteValueDictionary _routeValues;
        private readonly string _routeName;
        private readonly int _startPageIndex = 1;
        private readonly int _endPageIndex = 1;
        private readonly AjaxOptions _ajaxOptions;
        private readonly IDictionary<string, object> _htmlAttributes;

        /// <summary>
        ///     used when PagedList is null
        /// </summary>
        internal PagerBuilder(HtmlHelper html, AjaxHelper ajax, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes) {
            if (pagerOptions == null)
                pagerOptions = new PagerOptions();
            _html = html;
            _ajax = ajax;
            _pagerOptions = pagerOptions;
            _htmlAttributes = htmlAttributes;
        }

        internal PagerBuilder(HtmlHelper html, AjaxHelper ajax, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes) {
            if (String.IsNullOrEmpty(actionName)) {
                if (ajax != null)
                    actionName = (string) ajax.ViewContext.RouteData.Values["action"];
                else
                    actionName = (string) html.ViewContext.RouteData.Values["action"];
            }
            if (String.IsNullOrEmpty(controllerName)) {
                if (ajax != null)
                    controllerName = (string) ajax.ViewContext.RouteData.Values["controller"];
                else
                    controllerName = (string) html.ViewContext.RouteData.Values["controller"];
            }
            if (pagerOptions == null)
                pagerOptions = new PagerOptions();

            _html = html;
            _ajax = ajax;
            _actionName = actionName;
            _controllerName = controllerName;
            _totalPageCount = totalPageCount;
            _pageIndex = pageIndex;
            _pagerOptions = pagerOptions;
            _routeName = routeName;
            _routeValues = routeValues;
            _ajaxOptions = ajaxOptions;
            _htmlAttributes = htmlAttributes;

            // start page index
            _startPageIndex = pageIndex - (pagerOptions.NumericPagerItemCount/2);
            if (_startPageIndex + pagerOptions.NumericPagerItemCount > _totalPageCount)
                _startPageIndex = _totalPageCount + 1 - pagerOptions.NumericPagerItemCount;
            if (_startPageIndex < 1)
                _startPageIndex = 1;

            // end page index
            _endPageIndex = _startPageIndex + _pagerOptions.NumericPagerItemCount - 1;
            if (_endPageIndex > _totalPageCount)
                _endPageIndex = _totalPageCount;
        }

        //non Ajax pager builder
        internal PagerBuilder(HtmlHelper helper, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) : this(helper, null, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, null, htmlAttributes) {}
        //jQuery Ajax pager builder
        internal PagerBuilder(HtmlHelper helper, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes) : this(helper, null, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, ajaxOptions, htmlAttributes) {}

        private void AddPrevious(ICollection<PagerItem> results) {
            var item = new PagerItem(_pagerOptions.PrevPageText, _pageIndex - 1, _pageIndex == 1, PagerItemType.PrevPage);
            if (!item.Disabled || (item.Disabled && _pagerOptions.ShowDisabledPagerItems))
                results.Add(item);
        }

        private void AddFirst(ICollection<PagerItem> results) {
            var item = new PagerItem(_pagerOptions.FirstPageText, 1, _pageIndex == 1, PagerItemType.FirstPage);

            if (!item.Disabled || (item.Disabled && _pagerOptions.ShowDisabledPagerItems))
                results.Add(item);
        }

        private void AddMoreBefore(ICollection<PagerItem> results) {
            if (_startPageIndex > 1 && _pagerOptions.ShowMorePagerItems) {
                var index = _startPageIndex - 1;
                if (index < 1)
                    index = 1;
                var item = new PagerItem(_pagerOptions.MorePageText, index, false, PagerItemType.MorePage);
                results.Add(item);
            }
        }

        private void AddPageNumbers(ICollection<PagerItem> results) {
            for (var pageIndex = _startPageIndex; pageIndex <= _endPageIndex; pageIndex++) {
                var text = pageIndex.ToString();
                if (pageIndex == _pageIndex && !string.IsNullOrEmpty(_pagerOptions.CurrentPageNumberFormatString))
                    text = String.Format(_pagerOptions.CurrentPageNumberFormatString, text);
                else if (!string.IsNullOrEmpty(_pagerOptions.PageNumberFormatString))
                    text = String.Format(_pagerOptions.PageNumberFormatString, text);
                var item = new PagerItem(text, pageIndex, false, PagerItemType.NumericPage);
                results.Add(item);
            }
        }

        private void AddMoreAfter(ICollection<PagerItem> results) {
            if (_endPageIndex < _totalPageCount) {
                var index = _startPageIndex + _pagerOptions.NumericPagerItemCount;
                if (index > _totalPageCount)
                    index = _totalPageCount;
                var item = new PagerItem(_pagerOptions.MorePageText, index, false, PagerItemType.MorePage);
                results.Add(item);
            }
        }

        private void AddNext(ICollection<PagerItem> results) {
            var item = new PagerItem(_pagerOptions.NextPageText, _pageIndex + 1, _pageIndex >= _totalPageCount, PagerItemType.NextPage);
            if (!item.Disabled || (item.Disabled && _pagerOptions.ShowDisabledPagerItems))
                results.Add(item);
        }

        private void AddLast(ICollection<PagerItem> results) {
            var item = new PagerItem(_pagerOptions.LastPageText, _totalPageCount, _pageIndex >= _totalPageCount, PagerItemType.LastPage);
            if (!item.Disabled || (item.Disabled && _pagerOptions.ShowDisabledPagerItems))
                results.Add(item);
        }

        /// <summary>
        ///     generate paging url
        /// </summary>
        /// <param name="pageIndex"> page index to generate navigate url </param>
        /// <returns> navigated url for pager item </returns>
        private string GenerateUrl(int pageIndex) {
            //return null if  page index larger than total page count or page index is current page index
            if (pageIndex > _totalPageCount || pageIndex == _pageIndex)
                return null;

            var routeValues = _routeValues ?? new RouteValueDictionary();

            // set route value of page index parameter name in url,pageIndex==0 is a special case
            routeValues[_pagerOptions.PageIndexParameterName] = pageIndex;

            var rq = _html.ViewContext.HttpContext.Request.QueryString;
            foreach (string key in rq.Keys) {
                if (key != _pagerOptions.PageIndexParameterName)
                    routeValues[key] = rq[key];
            }
            // Add action
            routeValues["action"] = _actionName;

            // Add controller
            routeValues["controller"] = _controllerName;

            // Return link
            var urlHelper = new UrlHelper(_html.ViewContext.RequestContext);
            if (!string.IsNullOrEmpty(_routeName))
                return urlHelper.RouteUrl(_routeName, routeValues);
            return urlHelper.RouteUrl(routeValues);
        }

        /// <summary>
        ///     render paging control
        /// </summary>
        /// <returns> </returns>
        internal MvcHtmlString RenderPager() {
            //return null if total page count less than or equal to 1
            if (_totalPageCount <= 1 && _pagerOptions.AutoHide)
                return null;

            var pagerItems = new List<PagerItem>();
            //First page
            if (_pagerOptions.ShowFirstLast)
                AddFirst(pagerItems);

            // Prev page
            if (_pagerOptions.ShowPrevNext)
                AddPrevious(pagerItems);

            if (_pagerOptions.ShowNumericPagerItems) {
                if (_pagerOptions.AlwaysShowFirstLastPageNumber && _startPageIndex > 1)
                    pagerItems.Add(new PagerItem("1", 1, false, PagerItemType.NumericPage));

                // more page before numeric page buttons
                if (_pagerOptions.ShowMorePagerItems)
                    AddMoreBefore(pagerItems);

                // numeric page
                AddPageNumbers(pagerItems);

                // more page after numeric page buttons
                if (_pagerOptions.ShowMorePagerItems)
                    AddMoreAfter(pagerItems);

                if (_pagerOptions.AlwaysShowFirstLastPageNumber && _endPageIndex < _totalPageCount)
                    pagerItems.Add(new PagerItem(_totalPageCount.ToString(), _totalPageCount, false, PagerItemType.NumericPage));
            }

            // Next page
            if (_pagerOptions.ShowPrevNext)
                AddNext(pagerItems);

            //Last page
            if (_pagerOptions.ShowFirstLast)
                AddLast(pagerItems);

            var sb = new StringBuilder();
            if (_pagerOptions.UseJqueryAjax) {
                foreach (PagerItem item in pagerItems)
                    sb.Append(GenerateJqAjaxPagerElement(item));
            } else {
                foreach (PagerItem item in pagerItems)
                    sb.Append(GeneratePagerElement(item));
            }
            var tb = new TagBuilder(_pagerOptions.ContainerTagName);

            if (!string.IsNullOrEmpty(_pagerOptions.Id))
                tb.GenerateId(_pagerOptions.Id);
            if (!string.IsNullOrEmpty(_pagerOptions.CssClass))
                tb.AddCssClass(_pagerOptions.CssClass);

            tb.MergeAttributes(_htmlAttributes, true);

            sb.Length -= _pagerOptions.SeparatorHtml.Length;
            tb.InnerHtml = sb.ToString();
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.Normal));
        }

        private string GenerateAnchor(PagerItem item) {
            string url = GenerateUrl(item.PageIndex);
            if (_pagerOptions.UseJqueryAjax) {
                var scriptBuilder = new StringBuilder();
                //ignore OnSuccess property
                if (!string.IsNullOrEmpty(_ajaxOptions.OnFailure) || !string.IsNullOrEmpty(_ajaxOptions.OnBegin) || (!string.IsNullOrEmpty(_ajaxOptions.OnComplete) && _ajaxOptions.HttpMethod.ToUpper() != "GET")) {
                    scriptBuilder.Append("$.ajax({type:\'").Append(_ajaxOptions.HttpMethod.ToUpper() == "GET" ? "get" : "post");
                    scriptBuilder.Append("\',url:$(this).attr(\'href\'),success:function(data,status,xhr){$(\'#");
                    scriptBuilder.Append(_ajaxOptions.UpdateTargetId).Append("\').html(data);}");
                    if (!string.IsNullOrEmpty(_ajaxOptions.OnFailure))
                        scriptBuilder.Append(",error:").Append(HttpUtility.HtmlAttributeEncode(_ajaxOptions.OnFailure));
                    if (!string.IsNullOrEmpty(_ajaxOptions.OnBegin))
                        scriptBuilder.Append(",beforeSend:").Append(HttpUtility.HtmlAttributeEncode(_ajaxOptions.OnBegin));
                    if (!string.IsNullOrEmpty(_ajaxOptions.OnComplete))
                        scriptBuilder.Append(",complete:").Append(HttpUtility.HtmlAttributeEncode(_ajaxOptions.OnComplete));
                    scriptBuilder.Append("});return false;");
                } else {
                    if (_ajaxOptions.HttpMethod.ToUpper() == "GET") {
                        scriptBuilder.Append("$(\'#").Append(_ajaxOptions.UpdateTargetId);
                        scriptBuilder.Append("\').load($(this).attr(\'href\')");
                        if (!string.IsNullOrEmpty(_ajaxOptions.OnComplete))
                            scriptBuilder.Append(",").Append(HttpUtility.HtmlAttributeEncode(_ajaxOptions.OnComplete));
                        scriptBuilder.Append(");return false;");
                    } else {
                        scriptBuilder.Append("$.post($(this).attr(\'href\'), function(data) {$(\'#");
                        scriptBuilder.Append(_ajaxOptions.UpdateTargetId);
                        scriptBuilder.Append("\').html(data);});return false;");
                    }
                }
                return string.IsNullOrEmpty(url) ? _html.Encode(item.Text) : String.Format(CultureInfo.InvariantCulture, "<a href=\"{0}\" onclick=\"{1}\">{2}</a>", url, scriptBuilder, item.Text);
            }
            return "<a href=\"" + url + "\" onclick=\"window.open(this.attributes.getNamedItem('href').value,'_self')\"></a>";
        }

        private MvcHtmlString GeneratePagerElement(PagerItem item) {
            //pager item link
            string url = GenerateUrl(item.PageIndex);
            if (item.Disabled) //first,last,next or previous page
                return CreateWrappedPagerElement(item, String.Format("<a disabled=\"disabled\">{0}</a>", item.Text));
            return CreateWrappedPagerElement(item, string.IsNullOrEmpty(url) ? _html.Encode(item.Text) : String.Format("<a href='{0}'>{1}</a>", url, item.Text));
        }

        private MvcHtmlString GenerateJqAjaxPagerElement(PagerItem item) {
            if (item.Disabled)
                return CreateWrappedPagerElement(item, String.Format("<a disabled=\"disabled\">{0}</a>", item.Text));
            return CreateWrappedPagerElement(item, GenerateAnchor(item));
        }

        private MvcHtmlString CreateWrappedPagerElement(PagerItem item, string el) {
            string navStr = el;
            switch (item.Type) {
                case PagerItemType.FirstPage:
                case PagerItemType.LastPage:
                case PagerItemType.NextPage:
                case PagerItemType.PrevPage:
                    if ((!string.IsNullOrEmpty(_pagerOptions.NavigationPagerItemWrapperFormatString) || !string.IsNullOrEmpty(_pagerOptions.PagerItemWrapperFormatString)))
                        navStr = string.Format(_pagerOptions.NavigationPagerItemWrapperFormatString ?? _pagerOptions.PagerItemWrapperFormatString, el);
                    break;
                case PagerItemType.MorePage:
                    if ((!string.IsNullOrEmpty(_pagerOptions.MorePagerItemWrapperFormatString) || !string.IsNullOrEmpty(_pagerOptions.PagerItemWrapperFormatString)))
                        navStr = string.Format(_pagerOptions.MorePagerItemWrapperFormatString ?? _pagerOptions.PagerItemWrapperFormatString, el);
                    break;
                case PagerItemType.NumericPage:
                    if (item.PageIndex == _pageIndex && (!string.IsNullOrEmpty(_pagerOptions.CurrentPagerItemWrapperFormatString) || !string.IsNullOrEmpty(_pagerOptions.PagerItemWrapperFormatString))) //current page
                        navStr = string.Format(_pagerOptions.CurrentPagerItemWrapperFormatString ?? _pagerOptions.PagerItemWrapperFormatString, el);
                    else if (!string.IsNullOrEmpty(_pagerOptions.NumericPagerItemWrapperFormatString) || !string.IsNullOrEmpty(_pagerOptions.PagerItemWrapperFormatString))
                        navStr = string.Format(_pagerOptions.NumericPagerItemWrapperFormatString ?? _pagerOptions.PagerItemWrapperFormatString, el);
                    break;
            }
            return MvcHtmlString.Create(navStr + _pagerOptions.SeparatorHtml);
        }

        private RouteValueDictionary GetCurrentRouteValues(ViewContext viewContext) {
            var routeValues = _routeValues ?? new RouteValueDictionary();
            var rq = viewContext.HttpContext.Request.QueryString;
            foreach (string key in rq.Keys) {
                // add other url query string parameters (not include PageIndexParameterName parameter value and X-Requested-With=XMLHttpRequest ajax parameter) to route value collection
                if (key != _pagerOptions.PageIndexParameterName && (key.ToLower() != "x-requested-with" && rq[key].ToLower() != "xmlhttprequest"))
                    routeValues[key] = rq[key];
            }
            // action
            routeValues["action"] = _actionName;
            // controller
            routeValues["controller"] = _controllerName;
            return routeValues;
        }
    }
}