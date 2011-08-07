using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Runtime.Serialization.Json;

namespace MapItPrices.Models
{
    public class CommonDBActions
    {
        IMapItEntities MapItDB;
        User CurrentUser;

        public CommonDBActions(IMapItEntities entities, User currentUser)
        {
            MapItDB = entities;
            CurrentUser = currentUser;
        }

        public int CreateStore(Store storeToCreate)
        {
            try
            {
                Store store = new Store();

                store.Name = storeToCreate.Name;
                store.Address = storeToCreate.Address;
                store.City = storeToCreate.City;
                store.State = storeToCreate.State;
                store.Zip = storeToCreate.Zip;
                store.UserID = this.CurrentUser.ID;

                GeoCodeStore(store);
                MapItDB.Stores.AddObject(store);
                MapItDB.SaveChanges();

                return store.ID;
            }
            catch
            {
                return -1;
            }
        }

        public void GeoCodeStore(int StoreID)
        {
            GeoCodeStore(MapItDB.Stores.Single(s => s.ID == StoreID));
        }

        public static void GeoCodeStore(Store store)
        {
            string fullAddress = store.Address + ", " + store.City + ", " + store.State;

            string url = string.Format(
                "http://maps.google.com/maps/api/geocode/json?address={0}&region=dk&sensor=false",
                HttpUtility.HtmlEncode(fullAddress));

            var request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GeoResponse));
            var res = (GeoResponse)serializer.ReadObject(request.GetResponse().GetResponseStream());
            store.Latitude = res.Results[0].Geometry.Location.Lat;
            store.Longitude = res.Results[0].Geometry.Location.Lng;
        }

        public bool EditStore(Store newStoreValues)
        {
            try
            {
                var store = MapItDB.Stores.SingleOrDefault(s => s.ID == newStoreValues.ID);
                if (store != null)
                {
                    store.Name    = newStoreValues.Name;
                    store.Address = newStoreValues.Address;
                    store.City    = newStoreValues.City;
                    store.State   = newStoreValues.State;
                    store.Zip     = newStoreValues.Zip;
                    store.User    = this.CurrentUser;

                    MapItDB.SaveChanges();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}