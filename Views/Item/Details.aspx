<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MapItPrices.Models.Item>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

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
        
        <div class="display-label">UserID</div>
        <div class="display-field"><%: Model.UserID %></div>
        
        <div class="display-label">Brand</div>
        <div class="display-field"><%: Model.Brand %></div>
        
    </fieldset>
    <p>

        <%: Html.ActionLink("Edit", "Edit", new { id=Model.ID }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

