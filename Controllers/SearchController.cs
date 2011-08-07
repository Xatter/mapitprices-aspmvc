using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapItPrices.Models;
using MapItPrices.ViewModels;

namespace MapItPrices.Controllers
{
    public class SearchController : BaseController
    {
        //
        // GET: /Search/

        [HttpGet]
        public ActionResult Index(string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return View();
            }

            List<Item> items = new List<Item>();

            var searchstring = q.ToUpperInvariant();
            var words = searchstring.Split(' ');

            var startsWithResults = from i in MapItDB.Items
                                    where i.Name.ToUpper().StartsWith(searchstring) ||
                                    i.Brand.ToUpper().StartsWith(searchstring)
                                    select i;


            var containsResults = MapItDB.Items.Where(i => i.Name.ToUpper().Contains(searchstring) ||
                i.Brand.ToUpper().Contains(searchstring));

            items.AddRange(startsWithResults);
            items.AddRange(containsResults);
            items = items.Distinct().ToList();

            SearchResult result = new SearchResult(q, items);

            if (items.Count > 0)
            {
                var itemids = from i in items
                              select i.ID;

                var storeItems = from s in MapItDB.StoreItems
                                 where itemids.Contains(s.ItemId)
                                 select s;

                var thing = storeItems.AsEnumerable().ToList();
                result.StoreItems = storeItems.AsEnumerable();
            }

            return View(result);
        }

    }
}
