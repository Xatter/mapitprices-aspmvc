<%@ Page Title="" Language="C#" Debug="true" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MapItPrices.ViewModels.SearchResultViewModel>" %>

<%@ Import Namespace="MvcMaps" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Search Results
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Search Results</h2>

    <%
        var map = Ajax.GoogleMap().CssClass("GoogleMap grid_8");

        double firstLat = 40.7143528;
        double firstLong = -74.0059731;

        foreach (var searchResult in Model.SearchResult)
        {
            firstLong = searchResult.Store.Longitude ?? firstLat;
            firstLat = searchResult.Store.Latitude ?? firstLong;

            string description = string.Empty;

            foreach (var item in searchResult.Items)
            {
                description += Html.Encode(item.Item.Name.Trim() + ", " + item.Item.Size.Trim() + " - " + item.Price);
            }

            map.AddPushpin(
                new Pushpin(
                    firstLat,
                    firstLong,
                    searchResult.Store.Name,
                    description));
        }

        map.Center(firstLat, firstLong).Zoom(12).Render();
    %>

    <div class="grid_3">
        <div id="ItemList" class="grid_2">
            <table>
                <%foreach (var item in Model.Items)
                    {%>
                <tr><td><%:item.Name%>,</td><td><%:item.Size%></td></tr>
                <% } %>
            </table>
            <p>
                <%: Html.ActionLink("Create New", "Create", "Item") %>
            </p>
        </div>
        <div id="StructuredItemList" class="grid_3">
        <ul>
        <%foreach (var searchResult in Model.SearchResult) {%>
            <li><strong><%:searchResult.Store.Name %></strong></li>
            <ul>
            <%foreach (var item in searchResult.Items){%>
                <li><%:item.Item.Name %>, <%:item.Item.Size %>, <%:item.Price %> <%: Html.ActionLink("Update Price","UpdatePrice","Item") %></li>
              <%} %>
            </ul>
          <%} %>
          </ul>
        </div>
    </div>
</asp:Content>
