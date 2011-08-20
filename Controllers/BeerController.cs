using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MapItPrices.Controllers
{
    public class BeerController : BaseController
    {
        [HttpPost]
        public JsonResult GetStores(FormCollection collection)
        {
            double lat, lng;

            var stores = from s in MapItDB.Stores
                         select s;

            return Json(new
                {
                    Stores = stores
                });
        }


        [HttpPost]
        public JsonResult GetItems(FormCollection collection)
        {
            var category = MapItDB.Categories.SingleOrDefault(c => c.Name == "Beer");

            var items = from item in MapItDB.StoreItems
                        where item.Item.Categories.Contains(category)
                        select new
                        {
                            ID = item.Item.ID,
                            Name = item.Item.Name,
                            Size = item.Item.Size,
                            Brand = item.Item.Brand,
                            Price = item.Price
                        };

            return Json(items);
        }

    }
}
