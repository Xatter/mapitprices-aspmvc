<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MapItPrices.Models.BetaSignup>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Welcome to MapItPrices.com
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_12">
        <h3 class="top_navStyle">
            What is this?</h3>
        <p>
            MapItPrices.com takes all the known prices for an item and shows you on a map. Not
            just national prices, but the <i>bodega on the corner too!</i> It makes it easy
            for you to see which place has the best price and how far away that is from you.
            Just type in a search at the top for an item, then select the exact item off the
            list. Currently this only works in New York City. Although there is no particular
            reason that it won't work in your area.</p>
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
            <% Html.EnableClientValidation(); %>
            <% using (Html.BeginForm("Signup", "Home", FormMethod.Post))
               { %>

            <%: Html.ValidationSummary(true) %>
            <div>
                Email: <%: Html.TextBoxFor(m => m.Email) %>
                <input type="submit" value="Submit" /></div>
                <%:Html.ValidationMessageFor(m => m.Email) %>
            <% } %>
        </div>
        <h3 class="top_navStyle">
            Look Twitter!</h3>
        <script src="http://widgets.twimg.com/j/2/widget.js"></script>
        <script>
            new TWTR.Widget({
                version: 2,
                type: 'profile',
                rpp: 30,
                interval: 6000,
                width: 'auto',
                height: 200,
                theme: {
                    shell: {
                        background: '#099c2b',
                        color: '#ffffff'
                    },
                    tweets: {
                        background: '#ffffff',
                        color: '#18c724',
                        links: '#bd2cb3'
                    }
                },
                features: {
                    scrollbar: true,
                    loop: false,
                    live: false,
                    hashtags: true,
                    timestamp: true,
                    avatars: true,
                    behavior: 'all'
                }
            }).render().setUser('MapItPrices').start();
        </script>
    </div>
    <div class="grid_12">
        <div id="fb-root">
        </div>
        <script src="http://connect.facebook.net/en_US/all.js#xfbml=1"></script>
        <fb:like href="http://www.mapitprices.com" send="true" width="450" show_faces="true"
            font=""></fb:like>
    </div>
</asp:Content>
