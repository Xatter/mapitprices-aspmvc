<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
                        <%if ((bool)ViewData["IsAuthenticated"]) {  %>
                        Welcome,
                        <%: ViewData["RealName"] %>
                        <%:Html.ActionLink("Sign out", "Logout", "Users")%>
                        <table>
                            <tr>
                                <td> <%: Html.ActionLink("Report a Price","ReportPrice","Item") %> </td>
                                <td> <%: Html.ActionLink("New Store","Create","Store") %> </td>
                                <td> <%: Html.ActionLink("New Item","Create","Item") %> </td>
                                <td> <%: Html.ActionLink("Shopping Lists","Index","ShoppingList") %></td>
                                
                                <% if ((bool)ViewData["IsAdministrator"]) { %>
                                    <td><%: Html.ActionLink("Store Manager", "Index", "Store") %></td>
                                    <td><%: Html.ActionLink("Item Manager", "Index", "Item") %></td>
                                <% } %>
                            </tr>
                        </table>
                        <%} else { %>
                        <%: Html.ActionLink("Login", "Login", "Users")%>
                        <%} %>