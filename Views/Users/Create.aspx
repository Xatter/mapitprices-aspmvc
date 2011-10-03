<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MapItPrices.Models.OpenID>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>

            <%: Html.HiddenFor(model => model.ClaimedIdentifier) %>

            <div class="editor-label">
                <%: Html.LabelFor(model => model.User.Username) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.User.Username) %>
                <%: Html.ValidationMessageFor(model => model.User.Username) %>
            </div>            
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.User.Email) %>
            </div>

            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.User.Email) %>
                <%: Html.ValidationMessageFor(model => model.User.Email) %>
            </div>            

            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>
</asp:Content>

