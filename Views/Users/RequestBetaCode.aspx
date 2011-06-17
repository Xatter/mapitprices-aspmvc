<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MapItPrices.Models.BetaCodeRequest>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    RequestBetaCode
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        RequestBetaCode</h2>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <%: Html.HiddenFor(model => model.ClaimedIdentifier) %>
        <div>
            <p style="background-color: Red">
                <%: Model.ErrorMessage %></p>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.BetaCode) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.BetaCode) %>
            <%: Html.ValidationMessageFor(model => model.BetaCode) %>
        </div>
        <p>
            <input type="submit" value="Submit" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
