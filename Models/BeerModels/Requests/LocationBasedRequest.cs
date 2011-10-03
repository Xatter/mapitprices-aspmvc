using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapItPrices.Models.BeerModels.Requests
{
    public class LocationBasedRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}