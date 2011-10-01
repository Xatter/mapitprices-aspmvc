using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapItPrices.Models.Foursquare
{
    public class Venue
    {
        public String id {get; set;}
        public String name {get; set;}
        public Location location { get; set; }
        public VenueCategory[] categories { get; set; }
        public bool verified { get; set; }
    }
}
