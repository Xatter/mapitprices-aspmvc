<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MapItPrices.Models.Item>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>

            <%: Html.HiddenFor(model => model.ID) %> 
            <%: Html.HiddenFor(model => model.UserID) %>

            <div class="editor-label">
                <%: Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Name) %>
                <%: Html.ValidationMessageFor(model => model.Name) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.UPC) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.UPC) %>
                <%: Html.ValidationMessageFor(model => model.UPC) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Size) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Size) %>
                <%: Html.ValidationMessageFor(model => model.Size) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Brand) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Brand) %>
                <%: Html.ValidationMessageFor(model => model.Brand) %>
            </div>
            
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

