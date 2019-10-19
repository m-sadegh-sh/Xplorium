<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="zone-container">
    <%= Html.RouteLink("Web", "SearchWeb", new { @class = "web" })%>
    <%--    <%= Html.RouteLink("Images", "SearchEngineImages" + ((ViewData["SearchType"].ToString() == "Result") ? "Result" : string.Empty), ((ViewData["SearchType"].ToString() == "Result") ? new { query = ViewData["Query"], index = ViewData["Index"], size = ViewData["Size"] } : null), new { @class = "images" })%>
    <%= Html.RouteLink("Videos", "SearchEngineVideos" + ((ViewData["SearchType"].ToString() == "Result") ? "Result" : string.Empty), ((ViewData["SearchType"].ToString() == "Result") ? new { query = ViewData["Query"], index = ViewData["Index"], size = ViewData["Size"] } : null), new { @class = "videos" })%>
    <%= Html.RouteLink("News", "SearchEngineNews" + ((ViewData["SearchType"].ToString() == "Result") ? "Result" : string.Empty), ((ViewData["SearchType"].ToString() == "Result") ? new { query = ViewData["Query"], index = ViewData["Index"], size = ViewData["Size"] } : null), new { @class = "news" })%>
    <%= Html.RouteLink("Blogs", "SearchEngineBlogs" + ((ViewData["SearchType"].ToString() == "Result") ? "Result" : string.Empty), ((ViewData["SearchType"].ToString() == "Result") ? new { query = ViewData["Query"], index = ViewData["Index"], size = ViewData["Size"] } : null), new { @class = "blogs" })%>
    --%></div>
<script type="text/javascript">
    $(document).ready(function () {
        $("div.zone-container a.<%: ViewData["Active"] %>").addClass('active');
    });
</script>
