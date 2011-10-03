using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapItPrices.Models;

namespace MapItPrices.Models.BeerModels
{
    public class BeerStore
    {
        public BeerStore(Store store)
        {
            ID = store.ID;
            Name = store.Name;
            Longitude = store.Longitude ?? 0;
            Latitude = store.Latitude ?? 0;
            FoursquareVenueID = store.FoursquareVenueID;

            Address = new Address()
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
        public Address Address { get; set; }
        public string FoursquareVenueID { get; set; }
    }
}