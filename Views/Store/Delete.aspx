<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MapItPrices.Models.Store>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>

    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">ID</div>
        <div class="display-field"><%: Model.ID %></div>
        
        <div class="display-label">Name</div>
        <div class="display-field"><%: Model.Name %></div>
        
        <div class="display-label">GPS</div>
        <div class="display-field"><%: Model.GPS %></div>
        
        <div class="display-label">Address</div>
        <div class="display-field"><%: Model.Address %></div>
        
        <div class="display-label">City</div>
        <div class="display-field"><%: Model.City %></div>
        
        <div class="display-label">State</div>
        <div class="display-field"><%: Model.State %></div>
        
        <div class="display-label">Zip</div>
        <div class="display-field"><%: Model.Zip %></div>
        
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
            <input type="submit" value="Delete" /> |
            <%: Html.ActionLink("Back to List", "Index") %>
        </p>
    <% } %>

</asp:Content>

