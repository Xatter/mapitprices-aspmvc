using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapItPrices.ViewModels;
using MapItPrices.Models;
using System.Net;
using System.Runtime.Serialization.Json;
using MvcMaps;

namespace MapItPrices.Controllers
{
    public class MapController : BaseController
    {
        //
        // GET: /Map/1

        public ActionResult Index(int itemid)
        {
            // Make sure all the stores have GPS chords
            GeoCodeStoresWithoutGPS(itemid);

            var items = from i in this.MapItDB.StoreItems
                        where i.ItemId == itemid
                        select new StoreItemViewModel
                        {
                            ItemID = i.ItemId,
                            ItemName = i.Item.Name,
                            Price = i.Price,
                            Size = i.Item.Size,
                            StoreName = i.Store.Name,
                            StoreAddress = i.Store.Address,
                            Latitude = i.Store.Latitude,
                            Longitude = i.Store.Longitude,
                            LastUpdated = i.LastUpdated
                        };

            MapViewModel vm = new MapViewModel()
            {
                Item = this.MapItDB.Items.First(i => i.ID == itemid),
                StoreItems = items.AsEnumerable()
            };

            return View(vm);
        }


        private void GeoCodeStoresWithoutGPS(int itemid)
        {
            var stores = from i in this.MapItDB.StoreItems
                         where i.ItemId == itemid && i.Store.Latitude == null && i.Store.Longitude == null
                         select i.Store;

            foreach (var store in stores)
            {
                CommonDBActions.GeoCodeStore(store);
            }

            this.MapItDB.SaveChanges();
        }
    }
}