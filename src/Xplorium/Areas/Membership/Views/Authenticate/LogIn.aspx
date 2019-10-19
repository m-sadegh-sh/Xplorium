<%@ Page Language="C#" MasterPageFile="~/Areas/Membership/Views/Authenticate/Controller.Master"
    Inherits="ViewPage" %>

<asp:Content runat="server" ContentPlaceHolderID="TitleContent">
    Log In
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="CssContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="logo">
    </div>
    <%= Html.ValidationSummary() %>
    <div class="explore">
        <% using (Html.BeginRouteForm("MembershipAuthenticateLogIn", FormMethod.Post, new { @class = "sections" }))
           { %>
        <h1>
            Log In here!</h1>
        <div class="clear-fix">
        </div>
        <div class="section">
            <input name="username" type="text" get-focus="true" title="Type here username and hint enter" />
        </div>
        <div class="section">
            <input type="submit" value="LogIn" title="Log In" id="log-in" />
        </div>
        <%} %>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
    <%= Html.JavaScriptBlock("jquery/jquery.validate") %>
    <script type="text/javascript">
        $("form").validate({
            rules: { username: { required: true} }, messages: { username: "" }
        });
    </script>
</asp:Content>
