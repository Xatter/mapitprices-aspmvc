using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapItPrices.Models;

namespace MapItPrices.ViewModels
{
    public class ItemPriceUpdateViewModel
    {
        public IEnumerable<Store> Stores { get; set; }
        public StoreItem StoreItem { get; set; }
    }
}