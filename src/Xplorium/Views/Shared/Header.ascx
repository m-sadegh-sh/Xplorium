<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="header">
    <div>
        <ul>
            <li>
                <%= Html.RouteLink("EN", new { Culture = "en-gb" })%></li>
            <li>
                <%= Html.RouteLink("FA", new { Culture = "fa-ir" })%></li>
            <li>
                <%= Html.RouteLink("Preferences","")%></li>
        </ul>
        <span class="activity-indicator">
            <%= Preferences.Messages.ActivityIndicator %></span>
        <ul class="right">
            <%= Html.Xplorium().Account() %>
        </ul>
        <div class="clear-fix"></div>
    </div>
</div>
