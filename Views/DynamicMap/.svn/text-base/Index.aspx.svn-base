<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="MvcMaps" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    FindStore
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>FindStore</h2>
    <% Ajax.BingMap().CssClass("BingMap grid_12")
           .DynamicMap(new { controller = "DynamicMap", action = "FindStore" })
           .Render(); %>
</asp:Content>
