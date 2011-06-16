<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Welcome to MapItPrices.com
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_12">
        <h3 class="top_navStyle">
            What is this?</h3>
        <p>
            MapItPrices.com takes all the known prices for an item and shows you on a map. It
            makes it easy for you to see which place has the best price and how far away that
            is from you. Just type in a search at the top for an item, then select the exact
            item off the list. Currently this only works in New York City. Although there is
            no particular reason that it won't work in your area.</p>
        <h3 class="top_navStyle">
            Like Mobile?</h3>
        <p>
            Cool, so do we! That's why there's an Android Application to go along with it. This
            way you can keep your shopping list handy while you're on the go, including the
            map of where to go.</p>
        <h3 class="top_navStyle">
            Interested?</h3>
        <p>
            Sign up to be notified when the BETA starts:</p>
        <div>
            <% using (Html.BeginForm("Signup", "Home", FormMethod.Post))
               { %>
            <div>
                Email:<%:Html.TextBox("email",string.Empty) %>
                <input type="submit" value="Submit" /></div>
            <% } %>
        </div>
    </div>
</asp:Content>
