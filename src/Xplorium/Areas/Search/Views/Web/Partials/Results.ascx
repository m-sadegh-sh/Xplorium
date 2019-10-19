<%@ Control Language="C#" Inherits="ViewUserControl<PagedList<SearchResult>>" %>
<% foreach (var result in Model) %>
<% Html.RenderPartial("Partials/Result", result); %>
<div id="data-pager-container">
    <%= Html.AjaxPager(Model, new PagerOptions { Id = "data-pager-container", UseJqueryAjax = true }, new AjaxOptions() { UpdateTargetId = "results-wrapper" })%>
</div>
