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
        public JsonResult GetItemPrices(FormCollection collection)
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

        [HttpPost]
        public JsonResult GetAllItemsAtStore(FormCollection collection)
        {
            int storeid;
            if (!int.TryParse(collection["StoreID"], out storeid))
            {
                return Json(new { });
            }

            var items = from item in MapItDB.StoreItems
                         where item.StoreId == storeID &&
                         item.Item.Categories.Any(c => c.Name == "Beer")
                         select new
                         {
                             ID = item.Item.ID,
                             Name = item.Item.Name.Trim(),
                             Size = item.Item.Size.Trim(),
                             Brand = item.Item.Brand.Trim(),
                             StoreID = item.StoreId,
                             Price = item.Price
                         };

            return Json(items);
        }

        [HttpPost]
        public JsonResult GetAllItems()
        {
            var items = from item in MapItDB.Items
                        where item.Categories.Any(c => c.Name == "Beer")
                        select new
                        {
                            ID = item.ID,
                            Name = item.Name,
                            Size = item.Size,
                            Brand = item.Brand
                        };

            return Json(items);
        }

        [HttpPost]
        public JsonResult ReportPrice(FormCollection collection)
        {
            int itemid;
            if (!int.TryParse(collection["itemid"], out itemid))
            {
            return Json(new object { });
            }

            int storeid;
            if(!int.TryParse(collection["storeid"], out storeid))
            {
            return Json(new object { });
            }

            decimal price;
            if(!decimal.TryParse(collection["price"], out price))
            {
            return Json(new object { });
            }

            var storeitem = MapItDB.StoreItems.SingleOrDefault(s => s.ItemId == itemid && s.StoreId == storeid);
            if (storeitem == null)
            {
                storeitem = new StoreItem();
                storeitem.ItemId = itemid;
                storeitem.StoreId = storeid;
                storeitem.Price = price;
                storeitem.LastUpdated = DateTime.Now;
                MapItDB.StoreItems.Add(storeitem);
            }
            else
            {
                storeitem.Price = price;
                storeitem.LastUpdated = DateTime.Now;
            }

            MapItDB.SaveChanges();

            return Json(new object { });
        }
    }
}
