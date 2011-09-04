using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapItPrices.Models;
using MapItPrices.ViewModels;
using MapItPrices.Models.Attributes;

namespace MapItPrices.Controllers
{
    public class BeerController : BaseController
    {
        User _currentUser;

        public BeerController()
        {
            // some default user
            _currentUser = MapItDB.Users.SingleOrDefault(u => u.Email == "jim@mapitprices.com");
        }

        [HttpPost]
        [Compress]
        public JsonResult Login(FormCollection collection)
        {
            string sessionToken = collection["SessionToken"];
            User user = null;

            if (!string.IsNullOrEmpty(sessionToken))
            {
                user = MapItDB.Users.SingleOrDefault(u => u.SessionToken == sessionToken);
            }
            else
            {
                string username = collection["email"];
                string password = collection["password"];

                user = MapItDB.Users.SingleOrDefault(u => u.Email.ToUpper() == username.ToUpper());

                if (user == null)
                    return Json(null);

                if (user.Password != password)
                {
                    return Json(null);
                }

                //Save new session id
                user.SessionToken = Session.SessionID;
                MapItDB.SaveChanges();
            }

            return Json(new
            {
                ID = user.ID,
                Email = user.Email,
                SessionToken = user.SessionToken
            });
        }


        [HttpPost]
        [Compress]
        public JsonResult CreateUser(FormCollection collection)
        {
            string username = collection["email"];
            string password = collection["password"];

            var usercheck = MapItDB.Users.SingleOrDefault(u => u.Email.ToUpper() == username.ToUpper());

            if (usercheck == null)
            {
                User newUser = new User();
                newUser.Email = username;
                newUser.Password = password;

                MapItDB.Users.Add(newUser);
                newUser.SessionToken = Session.SessionID;
                MapItDB.SaveChanges();

                return Json(newUser);
            }
            else
            {
                return Json(null);
            }
        }

        [HttpPost]
        [Compress]
        public JsonResult GetItemPricesByUPC(FormCollection collection)
        {
            string upc = collection["upc"];

            var items = from i in MapItDB.StoreItems
                        where i.Item.UPC == upc
                        select new
                        {
                            ID = i.Item.ID,
                            Name = i.Item.Name,
                            Size = i.Item.Size,
                            Brand = i.Item.Brand,
                            StoreID = i.Store.ID,
                            Price = i.Price,
                            Quantity = i.Quantity
                        };

            return Json(items);

        }

        [HttpPost]
        [Compress]
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
                             Latitude = s.Latitude ?? -1,
                             Longitude = s.Longitude ?? -1,
                             Distance = 0.0,
                             Address = new
                             {
                                 Street = s.Address,
                             }
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
        [Compress]
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
        [Compress]
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

            return Json(items.OrderBy(i => i.Name));
        }

        [HttpPost]
        [Compress]
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

            return Json(items.OrderBy(i => i.Name));
        }

        [HttpPost]
        [Compress]
        public JsonResult ReportPrice(FormCollection collection)
        {
            checkAndSetCurrentUser(); // TODO: This should be an AuthorizationAttribute somehow

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
                storeitem.User = _currentUser;
                MapItDB.StoreItems.Add(storeitem);
            }
            else
            {
                storeitem.User = _currentUser;
                storeitem.Price = price;
                storeitem.Quantity = quantity;
                storeitem.LastUpdated = DateTime.Now;
            }

            MapItDB.SaveChanges();

            return Json(new
            {
                ItemId = storeitem.ItemId,
                StoreId = storeitem.StoreId,
                Price = storeitem.Price,
                Quantity = storeitem.Quantity,
                User = new
                {
                    ID = storeitem.User.ID
                },
                LastUpdated = storeitem.LastUpdated
            });
        }

        private void checkAndSetCurrentUser()
        {
            String sessionToken = this.Request.Headers["SessionToken"];
            if (!string.IsNullOrEmpty(sessionToken))
            {
                _currentUser = MapItDB.Users.SingleOrDefault(u => u.SessionToken == sessionToken);
            }
        }


        [HttpPost]
        [Compress]
        public JsonResult CreateStore(FormCollection collection)
        {
            checkAndSetCurrentUser(); // TODO: This should be an AuthorizationAttribute somehow

            // Required
            string storeName = collection["Name"];
            double lat = double.Parse(collection["Latitude"]);
            double lng = double.Parse(collection["Longitude"]);

            //optional
            string address = collection["Address.Street"];
            string city = collection["Address.City"];
            string state = collection["Address.State"];
            string zip = collection["Address.Zip"];

            Store store = new Store();
            store.Name = storeName.Trim();
            store.Longitude = lng;
            store.Latitude = lat;
            store.User = _currentUser;

            store.Address = address;
            store.City = city;
            store.State = state;
            store.Zip = zip;


            MapItDB.Stores.Add(store);
            MapItDB.SaveChanges();
            return Json(store);
        }


        [HttpPost]
        [Compress]
        public JsonResult CreateItem(FormCollection collection)
        {
            checkAndSetCurrentUser(); // TODO: This should be an AuthorizationAttribute somehow

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
            item.User = _currentUser;

            MapItDB.Items.Add(item);
            MapItDB.SaveChanges();
            return Json(item);
        }

        [HttpPost]
        [Compress]
        public JsonResult GetStore(FormCollection collection)
        {
            int storeid;
            if (!int.TryParse(collection["storeid"], out storeid))
            {
                return Json(new object { });
            }

            var store = MapItDB.Stores.SingleOrDefault(s => s.ID == storeid);
            if (store.Latitude == null && store.Longitude == null)
            {
                CommonDBActions.GeoCodeStore(store);
                MapItDB.SaveChanges();
            }

            return Json(new
            {
                ID = store.ID,
                Name = store.Name,
                Address = new { Street = store.Address },
                Longitude = store.Longitude,
                Latitude = store.Latitude
            });
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
