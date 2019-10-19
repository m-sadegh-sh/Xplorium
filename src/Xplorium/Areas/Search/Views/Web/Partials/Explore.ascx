<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="explore three-column">    
    <div class="sections">
        <h1>
            Features</h1>
        <div class="clear-fix">
        </div>
        <div class="section">
            <h2>
                <%: "{0} is based on".FormatWith(Preferences.Application.Name) %></h2>
            <p>
                ASP.NET MVC Framework 2.0 RTM, NET Frameword 4.0, LINQ To Sql, C#.NET 4.0, IIS 7.5, SQL
                Server 2008, HTML 5, CSS 3.0, jQuery and etc</p>
            <%= Html.RouteLink("Know more", "")%>
        </div>
        <div class="section">
            <h2>
                <%: "{0} Summaries".FormatWith(Preferences.Application.Name) %></h2>
            <p>
                <%= "{0}  now indexed {1}".FormatWith(Preferences.Application.Name, ViewData["Summaries"].ToString())%></p>
            <%= Html.RouteLink("Read more", "")%>
        </div>
        <div class="section">
            <h2>
                Feedback, Bug Report or
            </h2>
            <p>
                <%: "{0} is a student project and i’d love to hear what you think. I promise i’ll listen!".FormatWith(Preferences.Application.Name) %></p>
            <%= Html.RouteLink("Send now", "")%>
        </div>
    </div>
</div>
