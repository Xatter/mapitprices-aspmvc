﻿using System;
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
        User _androidUser;

        public BeerController()
        {
            _androidUser = MapItDB.Users.SingleOrDefault(u => u.Username == "android");
        }

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
                            Price = item.Price,
                            Quantity = item.Quantity
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
                        where item.StoreId == storeid &&
                        item.Item.Categories.Any(c => c.Name == "Beer")
                        select new
                        {
                            ID = item.Item.ID,
                            Name = item.Item.Name.Trim(),
                            Size = item.Item.Size.Trim(),
                            Brand = item.Item.Brand.Trim(),
                            UPC = item.Item.UPC.Trim(),
                            StoreID = item.StoreId,
                            Price = item.Price,
                            Quantity = item.Quantity
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
                            Name = item.Name.Trim(),
                            Size = item.Size.Trim(),
                            Brand = item.Brand.Trim(),
                            UPC = item.UPC.Trim()
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
            if (!int.TryParse(collection["storeid"], out storeid))
            {
                return Json(new object { });
            }

            decimal price;
            if (!decimal.TryParse(collection["price"], out price))
            {
                return Json(new object { });
            }

            int quantity;
            if (!int.TryParse(collection["quantity"], out quantity))
            {
                quantity = 1;
            }

            var storeitem = MapItDB.StoreItems.SingleOrDefault(s => s.ItemId == itemid && s.StoreId == storeid);
            if (storeitem == null)
            {
                storeitem = new StoreItem();
                storeitem.ItemId = itemid;
                storeitem.StoreId = storeid;
                storeitem.Price = price;
                storeitem.LastUpdated = DateTime.Now;
                storeitem.Quantity = quantity;
                storeitem.User = _androidUser;
                MapItDB.StoreItems.Add(storeitem);
            }
            else
            {
                storeitem.User = _androidUser;
                storeitem.Price = price;
                storeitem.Quantity = quantity;
                storeitem.LastUpdated = DateTime.Now;
            }

            MapItDB.SaveChanges();

            return Json(storeitem);
        }


        [HttpPost]
        public JsonResult CreateStore(FormCollection collection)
        {
            // Required
            string storeName = collection["name"];
            double lat = double.Parse(collection["lat"]);
            double lng = double.Parse(collection["lng"]);

            //optional
            string address = collection["address"];
            string city = collection["city"];
            string state = collection["state"];
            string zip = collection["zip"];

            Store store = new Store();
            store.Name = storeName.Trim();
            store.Longitude = lng;
            store.Latitude = lat;
            store.User = _androidUser;

            store.Address = address.Trim();
            store.City = city.Trim();
            store.State = state.Trim();
            store.Zip = zip.Trim();


            MapItDB.Stores.Add(store);
            MapItDB.SaveChanges();
            return Json(store);
        }


        [HttpPost]
        public JsonResult CreateItem(FormCollection collection)
        {
            string name = collection["Name"];
            if (string.IsNullOrEmpty(name))
            {
                return Json(new { });
            }
            var beerCategory = MapItDB.Categories.SingleOrDefault(c => c.Name == "Beer");

            string brand = collection["Brand"];
            string size = collection["Size"];
            string upc = collection["UPC"];

            Item item = new Item();
            item.Name = name.Trim();
            item.Brand = brand.Trim();
            item.Size = size.Trim();
            item.UPC = upc.Trim();
            item.Categories.Add(beerCategory);
            item.User = _androidUser;

            MapItDB.Items.Add(item);
            MapItDB.SaveChanges();
            return Json(item);
        }
    }
}
