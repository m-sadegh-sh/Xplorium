<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% var initializedValue = System.Convert.ToDateTime(ViewData.TemplateInfo.FormattedModelValue).ToLongDateString();%>
<%: Html.TextBox("", initializedValue)%>
<script type="text/javascript">
    $(document).ready(function () {
        $('#<%: ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty) %>').datepicker({ date: '<%: initializedValue %>', dateFormat: 'DD, MM dd, yy' });
    });
</script>
