using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapItPrices.ViewModels
{
    public class BeerStoreResult
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }

        public object Address { get; set; }
    }
}