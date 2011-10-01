using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapItPrices.Models.Foursquare;

namespace MapItPrices.Models.BeerModels.Requests
{
    public class ReportPriceRequest
    {
        public StoreItem item { get; set; }
        public Venue store { get; set; }
        public double newprice { get; set; }
    }
}