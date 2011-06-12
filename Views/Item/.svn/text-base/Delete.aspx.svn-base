<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MapItPrices.Models.Item>" %>

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
        
        <div class="display-label">UPC</div>
        <div class="display-field"><%: Model.UPC %></div>
        
        <div class="display-label">Size</div>
        <div class="display-field"><%: Model.Size %></div>
        
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
            <input type="submit" value="Delete" /> |
            <%: Html.ActionLink("Back to List", "Index") %>
        </p>
    <% } %>

</asp:Content>

