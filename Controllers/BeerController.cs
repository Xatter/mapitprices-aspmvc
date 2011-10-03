using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapItPrices.Models;
using MapItPrices.ViewModels;
using MapItPrices.Models.Attributes;
using MapItPrices.Models.Foursquare;
using MapItPrices.Models.BeerModels;
using MapItPrices.Models.BeerModels.Requests;
using MapItPrices.Models.BeerModels.Responses;

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
        public JsonResult FoursquareLogin(LoginRequest login)
        {
            User user = MapItDB.Users.SingleOrDefault(u => u.Email.ToUpper() == login.email.ToUpper());

            MapItResponse response = new MapItResponse();

            if (user != null)
            {
                response.Response.user = new BeerUser(user);
            }
            else
            {
                User newUser = new User();
                newUser.Email = login.email;
                newUser.Username = login.username;
                newUser.SessionToken = Session.SessionID;
                MapItDB.Users.Add(newUser);
                MapItDB.SaveChanges();

                response.Response.user = new BeerUser(newUser);
            }

            return Json(response);
        }

        [HttpPost]
        [Compress]
        public JsonResult Login2(LoginRequest login)
        {
            User user = null;
            MapItResponse response = new MapItResponse();

            // SessionToken login
            if (!string.IsNullOrEmpty(login.SessionToken))
            {
                user = MapItDB.Users.SingleOrDefault(u => u.SessionToken == login.SessionToken);
                if (user == null)
                {
                    response.Meta.Code = "401";
                    response.Meta.ErrorMessage = "Expired Session, please re-login";
                }
            }
            else
            {
                // Username/Password login
                user = MapItDB.Users.SingleOrDefault(u => u.Email.ToUpper() == login.email.ToUpper());

                if (user == null)
                {
                    response.Meta.Code = "401";
                    response.Meta.ErrorMessage = "Invalid Username/Password. Please try again.";
                    return Json(response);
                }

                if (user.Password != login.password)
                {
                    response.Meta.Code = "401";
                    response.Meta.ErrorMessage = "Invalid Username/Password. Please try again.";
                    return Json(response);
                }

                //Save new session id
                user.SessionToken = Session.SessionID;
                MapItDB.SaveChanges();
            }

            response.Response.user = new BeerUser(user);

            return Json(response);
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
                    return Json(new { });

                if (user.Password != password)
                {
                    return Json(new { });
                }

                //Save new session id
                user.SessionToken = Session.SessionID;
                MapItDB.SaveChanges();
            }

            //*sigh* I can't just return user whether it's null or not, because if it's not EF will throw
            // a circular reference error, so I need this check

            if (user == null)
                return Json(new { });

            return Json(new BeerUser(user));
        }

        [HttpPost]
        [Compress]
        public JsonResult CreateUser(FormCollection collection)
        {
            string email = collection["email"];

            if (!string.IsNullOrEmpty(email))
            {
                email = email.Trim();
            }

            string password = collection["password"];
            string username = collection["username"];

            var usercheck = MapItDB.Users.SingleOrDefault(u => u.Email.ToUpper() == email.ToUpper());

            if (usercheck == null)
            {
                User newUser = new User();
                newUser.Email = email;
                newUser.Password = password;
                newUser.Username = username;

                MapItDB.Users.Add(newUser);
                newUser.SessionToken = Session.SessionID;
                MapItDB.SaveChanges();

                return Json(new BeerUser(newUser));
            }
            else if (usercheck.OpenIDs != null)
            {
                // If email already exists and it has an OpenID then just update the password field
                usercheck.Password = password;
                usercheck.SessionToken = Session.SessionID;
                MapItDB.SaveChanges();

                return Json(new BeerUser(usercheck));
            }
            else
            {
                return Json(new { });
            }
        }

        [HttpPost]
        [Compress]
        public JsonResult CreateUser2(LoginRequest request)
        {
            MapItResponse response = new MapItResponse();
            var usercheck = MapItDB.Users.SingleOrDefault(u => u.Email.ToUpper() == request.email.ToUpper());

            if (usercheck == null)
            {
                usercheck = MapItDB.Users.SingleOrDefault(u => u.Username.ToUpper() == request.username.ToUpper());
                if (usercheck != null)
                {
                    response.Meta.Code = Meta.BADREQUEST;
                    response.Meta.ErrorMessage = "Username already exists. Please choose another.";
                    return Json(response);
                }

                User newUser = new User();
                newUser.Email = request.email;
                newUser.Password = request.password;
                newUser.Username = request.username;

                MapItDB.Users.Add(newUser);
                newUser.SessionToken = Session.SessionID;
                MapItDB.SaveChanges();

                response.Response.user = new BeerUser(newUser);
                response.Meta.Code = Meta.CREATED;

                return Json(response);
            }
            else if (usercheck.OpenIDs != null && usercheck.OpenIDs.Count > 0)
            {
                // If email already exists and it has an OpenID then just update the password field
                usercheck.Password = request.password;
                usercheck.SessionToken = Session.SessionID;
                MapItDB.SaveChanges();
                response.Response.user = new BeerUser(usercheck);
                return Json(response);
            }
            else
            {
                response.Meta.Code = Meta.BADREQUEST;
                response.Meta.ErrorMessage = "Email address already exists.";
                return Json(response);
            }            
        }

        [HttpPost]
        [Compress]
        public JsonResult GetItemPricesByUPC(FormCollection collection)
        {
            string upc = collection["upc"];

            var items = from i in MapItDB.StoreItems
                        where i.Item.UPC == upc && i.Item.Categories.Any(c => c.Name == "Beer")
                        select new
                        {
                            ID = i.Item.ID,
                            Name = i.Item.Name,
                            Size = i.Item.Size,
                            Brand = i.Item.Brand,
                            StoreID = i.Store.ID,
                            Price = i.Price,
                            Quantity = i.Quantity,
                            LastUpdated = i.LastUpdated
                        };

            return Json(items);

        }

        [HttpPost]
        [Compress]
        public JsonResult GetNearbyStores(FormCollection collection)
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

            var stores = from s in MapItDB.Stores.AsEnumerable()
                         select new BeerStore(s);

            List<BeerStore> storestoreturn = new List<BeerStore>();
            foreach (var store in stores)
            {
                if (store.Latitude == null || store.Longitude == null)
                {
                    GeoCodeStoresWithoutGPS(store);
                }
                store.Distance = Haversine.Distance(lat, lng, store.Latitude, store.Longitude);
                storestoreturn.Add(store);
            }

            return Json(storestoreturn.OrderBy(s => s.Distance));
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

            var stores = from s in MapItDB.StoreItems.AsEnumerable()
                         where s.Item.Categories.Any(c => c.Name == "Beer")
                         group s by s.Store into storegroup
                         select new BeerStore(storegroup.Key);

            List<BeerStore> storestoreturn = new List<BeerStore>();
            foreach (var store in stores)
            {
                if (store.Latitude == null || store.Longitude == null)
                {
                    GeoCodeStoresWithoutGPS(store);
                }

                store.Distance = Haversine.Distance(lat, lng, store.Latitude, store.Longitude);
                store.Distance *= 1000; // Kilometers to Meters
                storestoreturn.Add(store);
            }

            return Json(storestoreturn.OrderBy(s => s.Distance));
        }

        [HttpPost]
        [Compress]
        public JsonResult GetNearbyStores(LocationBasedRequest request)
        {
            MapItResponse response = new MapItResponse();

            MapItPrices.Models.Ellipsoid.BoundingBox box = Ellipsoid.FindBoundingBox(request.Latitude, request.Longitude, 9.0);

            var stores = from s in MapItDB.StoreItems.AsEnumerable()
                         where s.Item.Categories.Any(c => c.Name == "Beer") &&
                         s.Store.Latitude > box.LatMin &&
                         s.Store.Latitude < box.LatMax &&
                         s.Store.Longitude > box.LngMin &&
                         s.Store.Longitude < box.LngMax
                         group s by s.Store into storegroup
                         select new BeerStore(storegroup.Key);

            List<BeerStore> storestoreturn = new List<BeerStore>();
            foreach (var store in stores)
            {
                store.Distance = Haversine.Distance(request.Latitude, request.Longitude, store.Latitude, store.Longitude);
                store.Distance *= 1000; // Kilometers to Meters
                storestoreturn.Add(store);
            }

            response.Response.stores = storestoreturn.OrderBy(s => s.Distance).ToArray();
            return Json(response);
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
                            Quantity = item.Quantity,
                            LastUpdated = item.LastUpdated,
                            User = new
                            {
                                ID = item.User.ID,
                                Username = item.User.Username
                            }
                        };

            return Json(items.OrderBy(i => i.Price));
        }

        [HttpPost]
        [Compress]
        public JsonResult GetItemPrices2(LocationBasedRequest request)
        {
            MapItResponse response = new MapItResponse();

            MapItPrices.Models.Ellipsoid.BoundingBox box = Ellipsoid.FindBoundingBox(request.Latitude, request.Longitude, 9.0);

            // Do an in-memory JOIN so we can actually use new BeerItem()
            var users = MapItDB.Users;
            var items = from item in MapItDB.StoreItems.AsEnumerable()
                        where item.Item.Categories.Any(c => c.Name == "Beer") &&
                            item.Store.Latitude > box.LatMin &&
                            item.Store.Latitude < box.LatMax &&
                            item.Store.Longitude > box.LngMin &&
                            item.Store.Longitude < box.LngMax
                        join user in users on item.UserID equals user.ID
                        select new BeerItem(item, user);

            if (items.Count() == 0)
            {
                box = Ellipsoid.FindBoundingBox(40.75, -73.98, 9.0);
                items = from item in MapItDB.StoreItems.AsEnumerable()
                            where item.Item.Categories.Any(c => c.Name == "Beer") &&
                                item.Store.Latitude > box.LatMin &&
                                item.Store.Latitude < box.LatMax &&
                                item.Store.Longitude > box.LngMin &&
                                item.Store.Longitude < box.LngMax
                            join user in users on item.UserID equals user.ID
                            select new BeerItem(item, user);
            }
            response.Response.items = items.OrderBy(i => i.Price).ToArray();

            return Json(response);
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
                            Quantity = item.Quantity,
                            LastUpdated = item.LastUpdated,
                            User = new
                            {
                                ID = item.User.ID,
                                Username = item.User.Username
                            }
                        };

            return Json(items.OrderBy(i => i.Name));
        }

        [HttpPost]
        [Compress]
        public JsonResult GetAllItemsAtStore2(StoreItemsRequest request)
        {
            MapItResponse response = new MapItResponse();

            int storeid = request.StoreId;

            if (storeid == 0)
            {
                // that's a problem.
                response.Meta.Code = Meta.BADREQUEST;
                response.Meta.ErrorMessage = "invalid [StoreID=storeid]";
                return Json(response);
            }

            // Do an in-memory JOIN so we can actually use new BeerItem()
            var users = MapItDB.Users;
            var items = from item in MapItDB.StoreItems.AsEnumerable()
                        where item.StoreId == storeid &&
                        item.Item.Categories.Any(c => c.Name == "Beer")
                        join user in users on item.UserID equals user.ID
                        select new BeerItem(item, user);

            response.Response.items = items.OrderBy(i => i.Name).ToArray();

            return Json(response);
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
                            UPC = item.UPC.Trim(),
                            User = new
                            {
                                ID = item.User.ID,
                                Username = item.User.Username
                            }
                        };

            return Json(items.OrderBy(i => i.Name));
        }

        [HttpPost]
        [Compress]
        public JsonResult GetAllItems2()
        {
            var users = MapItDB.Users;

            // Using Linq to Objects here instead of Linq to Entities
            // as seen by the .AsEnumerable() call
            // This means we have to manually join the items to the users
            // list in order to get the complete information, and I believe
            // it means the join is happening in memory.  Probably not a big deal
            // until the dataset becomes much larger, then I'll have to look at this again.
            var items = from item in MapItDB.Items.AsEnumerable()
                        where item.Categories.Any(c => c.Name == "Beer")
                        join user in users on item.UserID equals user.ID
                        select new BeerItem(item, user);

            return Json(items.OrderBy(i => i.Name));
        }


        [HttpPost]
        [Compress]
        public JsonResult ReportPrice2(ReportPriceRequest request)
        {
            checkAndSetCurrentUser(); // TODO: This should be an AuthorizationAttribute somehow

            MapItResponse response = new MapItResponse();

            if (request.item.ItemId == 0)
            {

            }

            if (string.IsNullOrEmpty(request.store.id))
            {

            }

            if (!(request.newprice > 0))
            {

            }

            int quantity = request.item.Quantity ?? 0;
            if (quantity == 0)
                quantity = 1;

            Store store = MapItDB.Stores.SingleOrDefault(s => s.FoursquareVenueID == request.store.id);
            if (store == null)
            {
                store = new Store();
                store.Name = request.store.name;
                store.FoursquareVenueID = request.store.id;
                store.Address = request.store.location.address;
                store.City = request.store.location.city;
                store.State = request.store.location.state;
                store.Latitude = request.store.location.lat;
                store.Longitude = request.store.location.lng;
                store.User = _currentUser;

                MapItDB.Stores.Add(store);
            }

            StoreItem newPrice = MapItDB.StoreItems.SingleOrDefault(s => s.ItemId == request.item.ItemId && s.StoreId == store.ID);

            if (newPrice == null)
            {
                newPrice = new StoreItem();
                MapItDB.StoreItems.Add(newPrice);
            }

            newPrice.ItemId = request.item.ItemId;
            newPrice.Store = store;
            newPrice.User = _currentUser;
            newPrice.Price = (decimal)request.newprice;
            newPrice.Quantity = quantity;
            newPrice.LastUpdated = DateTime.Now;

            MapItDB.SaveChanges();

            response.Response.item = new BeerItem(newPrice, newPrice.User);
            response.Meta.Code = Meta.CREATED;


            return Json(response);
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

            if (price <= 0)
            {
                // We don't want negative, or 0 prices
                return Json(new { });
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

            return Json(new BeerItem(storeitem, storeitem.User));
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
            return Json(new
            {
                Name = store.Name,
                Address = new
                {
                    Street = store.Address,
                    City = store.City,
                    State = store.State,
                    Zip = store.Zip
                },
                Longitude = store.Longitude,
                Latitude = store.Latitude,
                User = new
                {
                    ID = store.User.ID,
                    Username = store.User.Username
                },
                Created = store.Created
            });
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
            return Json(new
            {
                ID = item.ID,
                Name = item.Name,
                Size = item.Size,
                UPC = item.UPC,
                LastUpdated = item.LastUpdated,
                User = new
                {
                    ID = item.User.ID,
                    Username = item.User.Username
                }
            });
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

        public JsonResult GetStore2(StoreRequest request)
        {
            MapItResponse response = new MapItResponse();

            if (request.StoreId == 0)
            {
                response.Meta.Code = Meta.BADREQUEST;
                response.Meta.ErrorMessage = "Bad Store Request [" + request.StoreId + "]";
            }

            var store = MapItDB.Stores.SingleOrDefault(s => s.ID == request.StoreId);
            if (store != null)
            {
                response.Response.store = new BeerStore(store);
            }

            return Json(response);
        }

        private void GeoCodeStoresWithoutGPS(BeerStore store)
        {
            CommonDBActions.GeoCodeStore(store);
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
