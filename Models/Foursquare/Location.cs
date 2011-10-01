using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapItPrices.Models.Foursquare
{
    public class Location
    {
        public String address { get; set; }
        public String crossStreet { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public double distance { get; set; }
        public String postalCode { get; set; }
        public String city { get; set; }
        public String state { get; set; }
    }
}