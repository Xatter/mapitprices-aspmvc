    $(function () {
        var map = new Microsoft.Maps.Map(document.getElementById('map_canvas'), {
            credentials: 'Al9ulWutfcB08NpQu_IKD2JmNlgG7f4za-PAyMmaluRh_RDhqiB8qx8qYHvZaH9B',
            showDashboard: false
        });

        map.setView({ zoom: 10, center: new Microsoft.Maps.Location(40.73, -73.98) })

        map.entities.clear();

        @foreach (var storeitem in Model.StoreItems)
        { 
        <text>
            var pushpin = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(@storeitem.Store.Latitude, @storeitem.Store.Longitude), {width: 200, text: '@storeitem.Store.Name' } );
            map.entities.push(pushpin);
        </text>
        }
    });