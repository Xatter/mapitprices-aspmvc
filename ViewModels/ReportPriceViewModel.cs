using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapItPrices.Models;

namespace MapItPrices.ViewModels
{
    public class ReportPriceViewModel
    {
        public ReportPriceViewModel(IEnumerable<Item> items, IEnumerable<Store> stores)
        {
            this.Items = items;
            this.Stores = stores;
        }

        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<Store> Stores { get; set; }
    }
}
