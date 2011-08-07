using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapItPrices.Models;

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

    }
}
