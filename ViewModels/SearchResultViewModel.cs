using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapItPrices.Models;

namespace MapItPrices.ViewModels
{
    public class SearchResultViewModel
    {
        public List<SearchResult> SearchResult { get; set; }
        public List<Item> Items { get; set; }
    }
}