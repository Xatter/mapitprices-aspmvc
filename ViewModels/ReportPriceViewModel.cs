﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapItPrices.Models;

namespace MapItPrices.ViewModels
{
    public class ReportPriceViewModel
    {
        public string StoreName { get; set; }
        public string ItemName { get; set; }
        public StoreItem StoreItem { get; set; }

        public ReportPriceViewModel()
        {
            this.StoreItem = new StoreItem();
        }
    }
}
