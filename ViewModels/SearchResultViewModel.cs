using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapItPrices.Models;

namespace MapItPrices.ViewModels
{
    public class SearchResultViewModel
    {
        public IEnumerable<SearchResult> SearchResult { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}