<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Xplorium.Model.Entities.Message>" %>
<div class="section">
    <h2>
        <%= Model.Title %>
    </h2>
    <p>
        <%= Model.Description %></p>
    <ul>
        <% foreach (string suggestion in Model.Suggestions as IList)
           { %>
        <li>
            <%= suggestion %></li>
        <%} %>
    </ul>
</div>
