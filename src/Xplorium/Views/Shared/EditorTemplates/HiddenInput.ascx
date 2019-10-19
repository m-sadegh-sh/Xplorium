<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script runat="server">
    private object ModelValue
    {
        get
        {
            //if (Model is System.Data.Linq.Binary)
            //{
            //    return System.Convert.ToBase64string(((System.Data.Linq.Binary)Model).ToArray());
            //}
            if (Model is System.Byte[])
            {
                return System.Convert.ToBase64String((byte[])Model);
            }
            return Model;
        }
    }
</script>
<% if (!ViewData.ModelMetadata.HideSurroundingHtml)
   { %>
<%= Html.Encode(ViewData.TemplateInfo.FormattedModelValue) %>
<% } %>
<%= Html.Hidden("", ModelValue) %>