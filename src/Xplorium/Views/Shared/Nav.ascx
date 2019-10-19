<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<ul id="Navigation" class="Navigation">
    <li>
        <%= Html.RenderAnchor("Search", "Spider").Spider()%>
    </li>
    <li>
        <%= Html.RenderAnchor("Search", "Search").Search()%>
    </li>
</ul>
