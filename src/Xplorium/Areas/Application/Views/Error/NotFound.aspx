<%@ Page Language="C#" MasterPageFile="~/Areas/Libraries/Views/Error/Default.Master" Inherits="ViewPage" %>

<asp:Content runat="server" ContentPlaceHolderID="TitleContent">
    Not Found
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <%= Html.StyleSheetBlock("xplorium/search/error/not-found") %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="error-container">
        
    </div>
</asp:Content>
