<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script runat="server">
    private bool? ModelValue
    {
        get
        {
            bool? value = null;
            if (ViewData.Model != null)
            {
                value = System.Convert.ToBoolean(ViewData.Model, System.Globalization.CultureInfo.InvariantCulture);
            }
            return value;
        }
    }
</script>
<% if (ViewData.ModelMetadata.IsNullableValueType)
   { %>
<select class="list-box tri-state" disabled="disabled">
    <option value="" <%= ModelValue.HasValue ? "" : "selected='selected'" %>>Not Set</option>
    <option value="true" <%= ModelValue.HasValue && ModelValue.Value ? "selected='selected'" : "" %>>
        True</option>
    <option value="false" <%= ModelValue.HasValue && !ModelValue.Value ? "selected='selected'" : "" %>>
        False</option>
</select>
<% }
   else
   { %>
<input class="check-box" disabled="disabled" type="checkbox" <%= ModelValue.Value ? "checked='checked'" : "" %> />
<% } %>