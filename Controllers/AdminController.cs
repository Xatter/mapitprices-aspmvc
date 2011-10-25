using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapItPrices.Models;

namespace MapItPrices.Controllers
{
    public class AdminController : Controller
    {
        MapItPricesEntities MapItDB;
        public AdminController()
        {
            MapItDB = new MapItPricesEntities();
        }

        //
        // GET: /Admin/

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult BeerItems()
        {
            var items = from item in MapItDB.StoreItems
                        where item.Item.Categories.Any(c => c.Name == "Beer")
                        orderby item.LastUpdated descending
                        select item;

            return View(items);
        }

        [Authorize]
        public ActionResult Users()
        {
            return View(MapItDB.Users.OrderByDescending(u => u.Created));
        }
    }
}
