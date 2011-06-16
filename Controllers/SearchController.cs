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

            model.SearchResult = new List<SearchResult>();
            model.Items = new List<Item>();

            var items = MapItDB.Items.Where(i => i.Name.ToUpper().Contains(search));

            return View(model);
        }
    }
}
