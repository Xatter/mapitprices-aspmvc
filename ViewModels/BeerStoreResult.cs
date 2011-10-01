using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapItPrices.Models;

namespace MapItPrices.ViewModels
{
    public class BeerStoreResult
    {
        public BeerStoreResult(Store store)
        {
            ID = store.ID;
            Name = store.Name;
            Longitude = store.Longitude ?? 0;
            Latitude = store.Latitude ?? 0;
            Address = new
            {
                Street = store.Address,
                City = store.City,
                State = store.State
            };
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }
        public object Address { get; set; }
    }
}