<%@ Page Language="C#" MasterPageFile="~/Areas/Search/Views/Web/Web.Master" Inherits="ViewPage<dynamic>" %>

<asp:Content runat="server" ContentPlaceHolderID="TitleContent">
    Xplorium.NET Web Search
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="CssContent">
    <%= Html.StyleSheetBlock("front-end/xplorium/box/default") %>
    <%= Html.StyleSheetBlock("front-end/xplorium/search/web/basic/default") %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div id="box-container">
        <div class="header">
            <h2>
                Xplorium.NET Web Search</h2>
            <h3>
                <%= ViewData["State"]%></h3>
        </div>
        <% using (Html.BeginRouteForm("SearchWeb", FormMethod.Get))
           { %>
        <div class="content">
            <div class="box">
                <div>
                    <span class="single">
                        <input name="q" id="q" type="text" title="Type here search term and hint enter" /></span>
                    <div class="clear-fix">
                    </div>
                </div>
            </div>
        </div>
        <div class="footer">
            <span>
                <%= Html.RouteLink("Advanced Web Search","SearchWebAdvanced") %></span>
            <input type="submit" title="Search" id="search" value="Search" />
            <div class="clear-fix">
            </div>
        </div>
        <%} %>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
    <%= Html.JavaScriptBlock("jquery/jquery.validate") %>
    <%= Html.JavaScriptBlock("xplorium/jquery/search/basic/default")%>
    <script type="text/javascript">
        $(document).ready(function () {
            $.search('<%= Url.RouteUrl("SearchWebJsonSuggestion") %>');
        });
    </script>
</asp:Content>
