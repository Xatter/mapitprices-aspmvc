using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapItPrices.Models;

namespace MapItPrices.ViewModels
{
    public class StoreViewModel
    {
        private string _gps;

        public string GPS
        {
            get { return _gps; }
            set
            {
                _gps = value;
                this.Longitude = double.Parse(_gps.Split(',')[1]);
                this.Latitude = double.Parse(_gps.Split(',')[0]);
            }
        }

        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string Name { get; set; }
    }
}