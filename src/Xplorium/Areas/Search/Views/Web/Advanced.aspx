<%@ Page Language="C#" MasterPageFile="~/Areas/Search/Views/Web/Web.Master" Inherits="ViewPage<SearchOption>" %>

<asp:Content runat="server" ContentPlaceHolderID="TitleContent">
    Xplorium.NET Advanced Web Search
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="CssContent">
    <%= Html.StyleSheetBlock("front-end/xplorium/wide-box/default") %>
    <%= Html.StyleSheetBlock("front-end/xplorium/search/web/advanced/default") %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div id="wide-box-container">
        <div class="header">
            <h2>
                Xplorium.NET Advanced Web Search</h2>
            <h3 id="result">
                Use the form below and your advanced search will appear here
            </h3>
        </div>
        <% using (Html.BeginRouteForm("SearchWeb", FormMethod.Get))
           { %>
        <div class="content">
            <div class="box">
                <div class="inline half">
                    <div class="group">
                        <label for="any">
                            One or more of these words:</label>
                        <span>
                            <input name="q" id="q" type="text" value="" class="get-focus at-least-one" />
                        </span>
                        <div class="clear-fix">
                        </div>
                    </div>
                    <div class="group last-child">
                        <label for="none">
                            None of these unwanted words:</label>
                        <span>
                            <input name="none" id="none" type="text" value="" class="at-least-one" />
                        </span>
                        <div class="clear-fix">
                        </div>
                    </div>
                </div>
                <div class="inline half">
                    <div class="group">
                        <label for="host">
                            On specific host:</label>
                        <span>
                            <input name="host" id="host" type="text" value="" class="at-least-one" />
                        </span>
                        <div class="clear-fix">
                        </div>
                    </div>
                    <div class="group last-child">
                        <label for="none">
                            None of these unwanted words:</label>
                        <span>
                            <input name="_" id="_" type="text" value="" />
                        </span>
                        <div class="clear-fix">
                        </div>
                    </div>
                </div>
            </div>
            <div class="box">
                <div class="inline half">
                    <div class="group">
                        <label for="per-page">
                            Results per page:</label>
                        <span>
                            <select name="size">
                                <option value="10" selected="selected">10</option>
                                <option value="20">20</option>
                                <option value="30">30</option>
                                <option value="50">50</option>
                                <option value="100">100</option>
                            </select>
                        </span>
                        <div class="clear-fix">
                        </div>
                    </div>
                    <div class="group last-child">
                        <label for="part">
                            Where your keywords show up:</label>
                        <span>
                            <select name="part">
                                <option value="0" selected="selected">Anywhere</option>
                                <option value="1">Url</option>
                                <option value="2">Title</option>
                                <option value="3">Description</option>
                                <option value="4">Keywords</option>
                                <option value="5">Content</option>
                            </select>
                        </span>
                        <div class="clear-fix">
                        </div>
                    </div>
                    <div class="clear-fix">
                    </div>
                </div>
            </div>
        </div>
        <div class="footer">
            <span>
                <%= Html.RouteLink("Web Search","SearchWeb") %></span>
            <input type="submit" title="Search" value="Search" />
            <div class="clear-fix">
            </div>
        </div>
        <%} %>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
    <%= Html.JavaScriptBlock("jquery/jquery.validate") %>
    <%= Html.JavaScriptBlock("xplorium/jquery/search/advanced/default")%>
    <script type="text/javascript">
        $(document).ready(function () {
            $.search('<%= Url.RouteUrl("SearchWebQueryAnalyze") %>');
        });
    </script>
</asp:Content>
