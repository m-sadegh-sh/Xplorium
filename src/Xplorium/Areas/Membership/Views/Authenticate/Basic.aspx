<%@ Page Language="C#" MasterPageFile="~/Areas/Search/Views/Web/Web.Master" Inherits="ViewPage" %>

<asp:Content runat="server" ContentPlaceHolderID="TitleContent">
    Web Search
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="CssContent">
    <%--<%= Html.StyleSheetBlock("xplorium/search-web-basic") %>--%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div id="search-container">
        <div class="header">
            <h2>
                Search</h2>
            <h3>
                some tips tricks or etc</h3>
        </div>
        <% using (Html.BeginRouteForm("SearchWebResult", FormMethod.Get, new { @class = "search-form" }))
           { %>
        <div class="content">
            <div class="box">
                <dl>
                    <dt>
                        <label for="q">
                            SS</label></dt>
                    <dd>
                        <input name="q" id="q" type="text" title="Type here search term and hint enter" /></dd>
                    <dd class="last-child">
                        <input name="q" type="text" title="Type here search term and hint enter" /></dd>
                    <div class="clear-fix">
                    </div>
                </dl>
                <%--<button type="submit" title="Search" id="search">
                    Search</button>--%>
            </div>
        </div>
        <div class="footer clear-fix">
            <span>
                <%= Html.RouteLink("Advanced Web Search","SearchWebAdvanced") %></span>
            <input type="submit" title="Search" id="search" value="SSS" class="search" />
        </div>
        <%} %>
    </div>
</asp:Content>
<%--<asp:Content ContentPlaceHolderID="ExploreContent" runat="server">
    <% Html.RenderPartial("partials/explore"); %>
</asp:Content>--%>
<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
    <%= Html.JavaScriptBlock("xplorium/search-web-basic") %>
    <%= Html.JavaScriptBlock("jquery/jquery.validate") %>
    <script type="text/javascript">
        $(document).ready(function () {
            $("input#q").autocomplete({
                source: '<%= Url.RouteUrl("SearchWebJsonSuggestion") %>',
                minLength: 0,
                delay: 1000
            });

            var form = $("form#searchWeb");
            var q = $("input#q");
            //            var tipsDialog = $("div#tipsDialog");
            //            var closeTimer = null;

            //            tipsDialog.dialog({ autoOpen: false, modal: true, show: "drop", hide: "drop", width: 400, height: 140,
            //                buttons: { "Try it now": function () { window.location = '<%= Url.RouteUrl("SearchWebAdvanced") %>'; },
            //                    "No thank's": function () { tipsDialog.dialog("close"); q.focus(); }
            //                },
            //                close: clearTimer,
            //                draggable: true
            //            });

            //            setTimeout(function () {
            //                tipsDialog.dialog("open");
            //                closeTimer = setTimeout(function () {
            //                    closeTimer = null;
            //                    tipsDialog.dialog("close");
            //                    q.focus();
            //                }, 7500);
            //            }, 7500);

            //            function clearTimer() {
            //                if (closeTimer) {
            //                    clearTimeout(closeTimer);
            //                    closeTimer = null;
            //                }
            //            };

            //            form.submit(function () {
            //                return QueryState();
            //            });

            q.change(function () {
                QueryState();
            });

            QueryState = function () {
                if (q.val() == '') {
                    q.addClass("highlight").focus();
                    return false;
                }
                return true;
            };


            $("form").validate({
                rules: { q: { required: true} }, messages: { q: "" }
            });

        });
    </script>
</asp:Content>
