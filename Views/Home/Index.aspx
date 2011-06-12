<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Welcome to MapItPrices.com
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_12">
        <h3 class="top_navStyle">
            What is this?</h3>
        <p>
            MapItPrices.com is a website that will take all the known prices for an item and
            lay them out on a map so it's easy for you to see which place has the best price
            and how far away that is from you. Just type in a search at the top for an item,
            then select the exact item you meant off the list.
        </p>
        <p>
            Currently this only really works in <strong>New York City.</strong> Although there
            is no particular reason that it won't work in your area, but there just won't be
            as much data.
        </p>
    </div>
</asp:Content>
