<%@ Page Language="C#" MasterPageFile="~/Areas/Search/Views/Web/Web.Master" Inherits="ViewPage<PagedList<SearchResult>>" %>

<asp:Content runat="server" ContentPlaceHolderID="TitleContent">
    <%: ViewData["q"] %>
    - Xplorium.NET Web Search
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="CssContent">
    <%= Html.StyleSheetBlock("front-end/xplorium/wide-box/default") %>
    <%= Html.StyleSheetBlock("front-end/xplorium/search/web/result/default") %>
    <%= Html.StyleSheetBlock("front-end/xplorium/data-pager/default") %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div id="wide-box-container">
        <div class="header">
            <h2>
                Xplorium.NET Web Search</h2>
            <h3>
                <strong>
                    <%: Model.StartRecordIndex %></strong> - <strong>
                        <%: Model.EndRecordIndex %></strong> of <strong>
                            <%: Model.TotalItemCount %></strong> results for <strong>
                                <%:ViewData["q"]%></strong> -
                <%: ViewData["Summary"] %></h3>
        </div>
        <% using (Html.BeginRouteForm("SearchWeb", FormMethod.Get))
           { %>
        <div class="content">
            <div class="box">
                <div>
                    <span class="single">
                        <input name="q" id="q" type="text" title="Type here search term and hint enter" value="<%: ViewData["q"] %>" />
                        <input type="submit" title="Search" value="Search" />
                    </span>
                    <div class="clear-fix">
                    </div>
                </div>
            </div>
        </div>
        <%} %>
        <div class="content-wrapper">
            <div id="results-wrapper" class="results-wrapper">
                <% Html.RenderPartial("Partials/Results", Model); %>
            </div>
            <div class="sidebar-wrapper">
                <div class="advertisement">
                    Advertisement
                </div>
            </div>
            <div class="clear-fix">
            </div>
        </div>
        <div class="footer">
            <div class="clear-fix">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
    <%= Html.JavaScriptBlock("jquery/jquery.validate") %>
    <%= Html.JavaScriptBlock("xplorium/jquery/search/result/default")%>
    <script type="text/javascript">
        $(document).ready(function () {
            $.search('<%= Url.RouteUrl("SearchWebJsonSuggestion") %>');
        });
    </script>
</asp:Content>
