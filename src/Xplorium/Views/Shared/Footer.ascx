<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="footer">
    <div>
        <ul>
            <li>
                <%= Html.ActionLink(string.Format("{0} Home", Preferences.Application.Name), "") %></li>
            <li>
                <%= Html.ActionLink("About", "") %></li>
            <li>
                <%= Html.ActionLink("Contact", "") %></li>
        </ul>
        <ul>
            <li><span>
                <%= Xplorium.Web.Preferences.Application.Copyright %></span> </li>
        </ul>
    </div>
</div>
