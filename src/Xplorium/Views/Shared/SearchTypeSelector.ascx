<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<ul>
    <li class="web current">
        <%= Html.RouteLink("Web", "SearchWeb")%>
    </li>
    <li class="images">
        <%= Html.RouteLink("Images", "SearchWeb")%>
    </li>
    <li class="videos">
        <%= Html.RouteLink("Videos", "SearchWeb")%>
    </li>
    <li class="news">
        <%= Html.RouteLink("News", "SearchWeb")%>
    </li>
    <li class="blogs">
        <%= Html.RouteLink("Blogs", "SearchWeb")%>
    </li>
</ul>
<script type="text/javascript">
    $("ul li").addClass("current");
</script>
