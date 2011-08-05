<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MapItPrices.ViewModels.MapViewModel>" %>
<%@ Import Namespace="MvcMaps" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Map - <%: Model.Item.Name %> - <%: Model.Item.Size %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Map - <%: Model.Item.Name %> - <%: Model.Item.Size %></h2>

    <%
        var map = Ajax.GoogleMap().CssClass("GoogleMap grid_8");

        double firstLat= 40.7143528;
        double firstLong= -74.0059731;

        foreach (var item in Model.StoreItems)
        {
            firstLong = item.Longitude ?? firstLat;
            firstLat = item.Latitude ?? firstLong;
            
            map = map.AddPushpin(
                new Pushpin(
                    item.Latitude ?? firstLat,
                    item.Longitude ?? firstLong,
                    item.StoreName, 
                    item.StoreName + '-' + String.Format("{0:c}",item.Price + " Updated: " + item.LastUpdated + "<br/>")));
        }
        
        map.Center(firstLat, firstLong).Zoom(14).Render();
    %>

    <div id="ItemList" class="grid_2">
        <table>
       <%foreach (var item in Model.StoreItems) { %>
       <tr><td><%: item.StoreName %></td><td><%: String.Format("{0:c}",item.Price) %></td><td><%: item.LastUpdated %></td></tr>
       <% } %>
        </table>
    </div>
</asp:Content>
