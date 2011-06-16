using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapItPrices.Models;

namespace MapItPrices.ViewModels
{
    public class SearchResult
    {
        public Store Store { get; set; }
        public List<StoreItem> Items { get; set; }
    }
}
