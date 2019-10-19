<%@ Page Language="C#" MasterPageFile="~/Areas/Membership/Views/Authenticate/Controller.Master"
    Inherits="ViewPage" %>

<asp:Content runat="server" ContentPlaceHolderID="TitleContent">
    You are already logged in
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="CssContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="logo">
    </div>    
    <div class="explore">
        <% using (Html.BeginRouteForm("MembershipAuthenticateLogOut", FormMethod.Post, new { @class = "sections" }))
           { %>
        <h1>
            You are already logged in!</h1>
        <div class="clear-fix">
        </div>
        <div class="section">
            <p>Click log out to logging out</p>
        </div>
        <div class="section">
            <input type="submit" value="Log Out" title="Log Out" id="log-out" />
        </div>
        <%} %>
    </div>
</asp:Content>