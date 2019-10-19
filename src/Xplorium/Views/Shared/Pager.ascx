<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%= Html.DataPager((int)ViewData["TotalRecords"], 10, 7, "«", "»", (i) => Url.RouteUrl("Paginated", new { PageIndex = i }))%>