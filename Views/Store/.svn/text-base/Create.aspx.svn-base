<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MapItPrices.Models.Store>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
    <% Html.EnableClientValidation(); %>

    <h2>Add New Store</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
                        
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Name) %>
                <%: Html.ValidationMessageFor(model => model.Name) %>
            </div>
                        
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Address) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Address) %>
                <%: Html.ValidationMessageFor(model => model.Address) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.City) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.City) %>
                <%: Html.ValidationMessageFor(model => model.City) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.State) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.State) %>
                <%: Html.ValidationMessageFor(model => model.State) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Zip) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Zip) %>
                <%: Html.ValidationMessageFor(model => model.Zip) %>
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

