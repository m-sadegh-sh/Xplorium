<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%= Html.TextArea("", ViewData.TemplateInfo.FormattedModelValue.ToString(),
	                  10, 60, new { @class = "text-box multi-line wymeditor"}) %>
