<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% if (ViewData.TemplateInfo.TemplateDepth > 1)
   { %>
<%= ViewData.ModelMetadata.SimpleDisplayText %>
<% }
   else
   { %>
<div>
    <% foreach (var prop in ViewData.ModelMetadata.Properties.Where(pm => pm.ShowForEdit && !ViewData.TemplateInfo.Visited(pm)))
       { %>
    <% if (prop.HideSurroundingHtml)
       { %>
    <%= Html.Editor(prop.PropertyName) %>
    <% }
       else
       { %>
    <div id="<%: (prop.PropertyName.ToSlug() + "-box") %>" class="editor-box<%: prop.IsRequired ? " required" : "" %>">
        <div class="editor-row">
            <div class="editor-label">
                <%= Html.Label(prop.PropertyName) %></div><div class="editor-field">
                <%= Html.Editor(prop.PropertyName) %></div>
        </div>
        <div class="editor-row">
            <div class="editor-validation-message">
                <%= Html.ValidationMessage(prop.PropertyName, prop.NullDisplayText) %></div>
        </div>
    </div>
    <% } %>
    <% } %>
</div>
<% } %>