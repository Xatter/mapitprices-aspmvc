using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapItPrices.Models.Foursquare
{
    public class VenueCategory
    {
        public String id { get; set; }
        public String name { get; set; }
        public String pluralName { get; set; }
        public String shortName { get; set; }
        public String icon { get; set; }
        public String[] parents { get; set; }
        public bool primary { get; set; }
    }
}
