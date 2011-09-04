using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapItPrices.Models;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Web.Helpers;

namespace MapItPrices.Controllers
{
    public class APIController : BaseController
    {
        User _currentUser;
        CommonDBActions _db;

        public APIController()
            : base()
        {
            _currentUser = MapItDB.Users.SingleOrDefault(u => u.Email == "jim@mapitprices.com");
            _db = new CommonDBActions(this.MapItDB, _currentUser);
        }

        public ActionResult SearchStores(string term)
        {
            var data = from i in MapItDB.Stores
                       where i.Name.ToUpper().Contains(term.ToUpper())
                       select new
                       {
                           i.ID,
                           i.Name,
                           i.Address,
                           i.City,
                           i.State
                       };

            return new ObjectResult(data.AsEnumerable());
        }

        //
        // GET: /API/SearchItems?searchtext={searchtext}
        // 
        public ActionResult SearchItems(string term)
        {
            // check if it's a UPC
            if (IsStringCompletelyNumeric(term))
            {
                var items = from i in MapItDB.Items
                            where i.UPC.Trim() == term.Trim()
                            select new
                            {
                                ID = i.ID,
                                Name = i.Name,
                                UPC = i.UPC,
                                Size = i.Size
                            };

                return new ObjectResult(items);
            }
            else
            {
                var upperTerm = term.ToUpperInvariant();

                var items = from i in MapItDB.Items
                            where i.Name.ToUpper().Contains(upperTerm) ||
                            i.Brand.ToUpper().Contains(upperTerm)
                            select new
                            {
                                ID = i.ID,
                                Name = i.Name,
                                Brand = i.Brand,
                                UPC = i.UPC,
                                Size = i.Size
                            };

                return new ObjectResult(items);
            }
        }

        private static bool IsStringCompletelyNumeric(string searchtext)
        {
            for (int i = 0; i < searchtext.Length; i++)
            {
                if ((int)searchtext[i] > 57 ||
                    (int)searchtext[i] < 48)
                    return false;
            }

            return true;
        }

        [HttpGet]
        public ActionResult SearchForMapResult(string searchtext)
        {
            try
            {
                var search = searchtext.ToUpperInvariant();

                var results = from storeitem in MapItDB.StoreItems
                              where storeitem.Item.Name.ToUpperInvariant().Contains(search)
                              //group storeitem by storeitem.StoreId into j
                              select new
                              {
                                  StoreName = storeitem.Store.Name,
                                  StoreID = storeitem.StoreId,
                                  ItemName = storeitem.Item.Name,
                                  ItemID = storeitem.ItemId,
                                  Price = storeitem.Price,
                                  Latitude = storeitem.Store.Latitude,
                                  Longitude = storeitem.Store.Longitude
                              };

                return new ObjectResult(new APICallResult(1, "Success", results));

            }
            catch
            {
                return new ObjectResult(new APICallResult(0, "Failed"));
            }
        }

        //
        // GET: /API/GetItem/{id}
        // 
        public ActionResult GetItem(int id)
        {
            try
            {
                var result = from i in MapItDB.Items
                             where i.ID == id
                             select new
                             {
                                 ID = i.ID,
                                 Name = i.Name,
                                 UPC = i.UPC,
                                 Size = i.Size
                             };

                return new ObjectResult(new APICallResult(1, "Success.", result));
            }
            catch
            {
                return new ObjectResult(new APICallResult(0, "Failed."));
            }
        }

        //
        // POST: /API/CreateItem/{id}
        // 
        [HttpPost]
        public JsonResult CreateItem(Item item)
        {
            CommonDBActions _db = new CommonDBActions(this.MapItDB, this.CurrentUser);
            var id = _db.CreateItem(item);
            return Json(new
            {
                Success = id != -1,
                ID = id,
                Item = item
            });
        }

        public ActionResult GetItemPrices(int id)
        {

            var itemPrices = from i in MapItDB.StoreItems
                             where i.ItemId == id
                             select new
                             {
                                 StoreID = i.StoreId,
                                 StoreName = i.Store.Name,
                                 ItemName = i.Item.Name,
                                 Price = i.Price,
                                 LastUpdated = i.LastUpdated,
                                 Latitude = i.Store.Latitude,
                                 Longitude = i.Store.Longitude
                             };

            return new ObjectResult(new APICallResult(1, "Success", itemPrices));
        }

        // 
        // POST: /API/EditItem/{id}
        //
        [HttpPost]
        public ActionResult EditItem(int id, FormCollection collection)
        {
            try
            {
                var item = MapItDB.Items.Single(i => i.ID == id);
                item.Name = collection["Name"];
                item.UPC = collection["UPC"];
                item.Size = collection["Size"];

                MapItDB.SaveChanges();

                return new ObjectResult(new APICallResult(1, "Success"));
            }
            catch
            {
                return new ObjectResult(new APICallResult(0, "Failure"));
            }
        }

        //
        // POST: /API/DeleteItem/{Id}
        //
        [HttpPost]
        public ActionResult DeleteItem(int id, FormCollection collection)
        {
            try
            {
                var item = MapItDB.Items.Single(i => i.ID == id);
                MapItDB.Items.Remove(item);
                MapItDB.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UpdatePrice(int itemid, int storeid, double price)
        {
            try
            {
                var itemprice = MapItDB.StoreItems.SingleOrDefault(i => i.ItemId == itemid && i.StoreId == storeid);

                if (itemprice == null)
                {
                    StoreItem storeitem = new StoreItem();
                    storeitem.ItemId = itemid;
                    storeitem.StoreId = storeid;
                    storeitem.Price = (decimal)price;
                    storeitem.LastUpdated = DateTime.Now;

                    MapItDB.StoreItems.Add(storeitem);
                }
                else
                {
                    itemprice.Price = (decimal)price;
                    itemprice.LastUpdated = DateTime.Now;
                }

                MapItDB.SaveChanges();

                return new ObjectResult(new APICallResult(1, "Success"));
            }
            catch
            {
                return new ObjectResult(new APICallResult(0, "Failure"));
            }
        }

        public ActionResult GetStore(int id)
        {
            try
            {
                var result = from s in MapItDB.Stores
                             where s.ID == id
                             select new
                             {
                                 Name = s.Name,
                                 Address = s.Address + ", " + s.City + ", " + s.State,
                                 Latitude = s.Latitude,
                                 Longitude = s.Longitude
                             };

                return new ObjectResult(new APICallResult(1, "Success.", result));
            }
            catch
            {

                return new ObjectResult(new APICallResult(0, "Failed."));
            }
        }

        [HttpPost]
        public JsonResult CreateStore(Store storeToCreate)
        {
            int id = _db.CreateStore(storeToCreate);
            return Json(new
                {
                    Success= id != -1,
                    ID = id,
                    Store = storeToCreate
                });
        }

        public ActionResult GetStores(double lat, double lng)
        {
            try
            {
                var result = from s in MapItDB.Stores
                             select new
                             {
                                 ID = s.ID,
                                 Name = s.Name,
                                 Address = s.Address,
                                 Latitude = s.Latitude,
                                 Longitude = s.Longitude
                             };

                return new ObjectResult(new APICallResult(1, "Success.", result));
            }
            catch
            {
                return new ObjectResult(new APICallResult(0, "Failed."));
            }
        }
    }
}