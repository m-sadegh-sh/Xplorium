<%@ Control Language="C#" Inherits="ViewUserControl<SearchResult>" %>
<div class="result">
    <% var q = ViewData["q"].ToString(); %>
    <div>
        <h2>
            <%= Html.RouteLink(Model.Title.ToStrong(q), "SearchWebTracker", new { UrlId = Model.UrlId }, new { title = Model.Title })%>
        </h2>
        <p>
            <%= Model.Description.ToStrong(q)%>
        </p>
        <%= Html.RouteLink(Model.Url.ToStrong(q), "SearchWebTracker", new { UrlId = Model.UrlId }, new { title = Model.Url })%>
    </div>
    <ul>
        <li>
            <%= Model.IndexedOn.ToLongDateString() %></li>
        <li>More on
            <%= Html.RouteLink(Model.Host.ToStrong(q), "")%></li>
        <li>
            <%= Html.RouteLink("See cached version", "SearchWebCache", new { CacheId = Model.UrlId })%></li>
        <li>Rate:
            <%: Model.Rate %></li>
        <li>Length:
            <%: Model.Length.FormatBytes() %></li>
    </ul>
    <div class="clear-fix">
    </div>
</div>
