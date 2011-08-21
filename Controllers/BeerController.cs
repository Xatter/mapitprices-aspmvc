using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapItPrices.Models;
using MapItPrices.ViewModels;

namespace MapItPrices.Controllers
{
    public class BeerController : BaseController
    {
        [HttpPost]
        public JsonResult GetStores(FormCollection collection)
        {
            double lat, lng;

            if (!double.TryParse(collection["Lat"], out lat))
            {
                return Json(new { });
            }

            if (!double.TryParse(collection["Lng"], out lng))
            {
                return Json(new { });
            }

            var stores = from s in MapItDB.Stores
                         select new BeerStoreResult
                         {
                             ID = s.ID,
                             Name = s.Name,
                             Latitude = s.Latitude,
                             Longitude = s.Longitude,
                             Distance = 0.0
                         };

            List<BeerStoreResult> storestoreturn = new List<BeerStoreResult>();
            foreach (var store in stores)
            {
                store.Distance = Haversine.Distance(lat, lng, store.Latitude, store.Longitude);
                storestoreturn.Add(store);
            }

            return Json(storestoreturn.OrderBy(s => s.Distance));
        }


        [HttpPost]
        public JsonResult GetItems(FormCollection collection)
        {
            var items = from item in MapItDB.StoreItems
                        where item.Item.Categories.Any(c => c.Name == "Beer")
                        select new
                        {
                            ID = item.Item.ID,
                            Name = item.Item.Name.Trim(),
                            Size = item.Item.Size.Trim(),
                            Brand = item.Item.Brand.Trim(),
                            StoreID = item.StoreId,
                            Price = item.Price
                        };

            return Json(items.OrderBy(i => i.Price));
        }
    }
}
