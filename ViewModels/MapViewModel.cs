using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapItPrices.Models;

namespace MapItPrices.ViewModels
{
    public class MapViewModel
    {
        public Item Item { get; set; }
        public IEnumerable<StoreItemViewModel> StoreItems { get; set; }
    }
}