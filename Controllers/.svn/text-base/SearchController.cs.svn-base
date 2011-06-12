using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapItPrices.Models;
using System.Collections;
using MapItPrices.ViewModels;

namespace MapItPrices.Controllers
{
    public class SearchController : BaseController
    {

        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index(string itemsearchtext)
        {
            var search = itemsearchtext.ToUpperInvariant();

            SearchResultViewModel model = new SearchResultViewModel();
            var items = MapItDB.Items.ToList();

            List<Item> matchingItems = new List<Item>();
            List<SearchResult> searchResults = new List<SearchResult>();

            // Do basic Contains search
            foreach (var item in items)
            {
                var itemSearchText = item.Name.ToUpperInvariant() + "_" + item.Brand.ToUpperInvariant() + "_" + item.UPC;

                if (itemSearchText.Contains(search))
                {
                    matchingItems.Add(item);
                    var thing = item.StoreItems.ToList();
                    var searchResult = from i in item.StoreItems
                                       group i by i.Store into g
                                       select new SearchResult
                                       {
                                           Store = g.Key,
                                           Items = g.Select(s => s).AsEnumerable()
                                       };
                    searchResults.AddRange(searchResult);
                }
            }

            // Do the joins to get all the metadata in
            model.SearchResult = searchResults;
            model.Items = (from i in MapItDB.Items
                           where i.Name.Contains(search)
                           select i).AsEnumerable();

            return View(model);
        }
    }
}
