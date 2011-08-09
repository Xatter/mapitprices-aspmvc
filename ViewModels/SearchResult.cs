using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapItPrices.Models;
using MapItPrices.Controllers;

namespace MapItPrices.ViewModels
{
    public class SearchResult
    {
        public string Query { get; private set; }
        public IEnumerable<Item> Items { get; private set; }
        public IEnumerable<StoreItem> StoreItems { get; set; }

        public SearchResult(string q, IEnumerable<Item> results)
        {
            this.Query = q;
            this.Items = results;
        }

        public IQueryable<IGrouping<Store, StoreItem>> GroupedByStore { get; set; }
    }
}
