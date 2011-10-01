using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapItPrices.Models.Foursquare
{
    public class FoursquareUser
    {
        public string username { get; set; }
        public string email { get; set; }
        public string foursquaretoken { get; set; }
        public string sessiontoken { get; set; }
    }
}
