using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapItPrices.Models;
using System.Net;
using System.Runtime.Serialization.Json;

namespace MapItPrices.Controllers
{
    public class StoreController : BaseController
    {
        public StoreController()
            : base()
        {
        }

        public StoreController(IMapItEntities entities)
            : base(entities)
        {
        }

        //
        // GET: /Store/

        public ActionResult Index()
        {
            var stores = MapItDB.Stores.ToList();
            return View(stores);
        }

        //
        // GET: /Store/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Store/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Store/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(FormCollection collection)
        {
            Store store = StoreFromFormCollection(collection);

            GeoCodeStore(store);

            MapItDB.Stores.AddObject(store);
            MapItDB.SaveChanges();

            return RedirectToAction("Index", "Store");
        }

        private Store StoreFromFormCollection(FormCollection collection)
        {
            Store store = new Store();

            store.Name = collection["Name"];
            store.Address = collection["Address"];
            store.City = collection["City"];
            store.State = collection["State"];
            store.Zip = collection["Zip"];
            store.User = this.CurrentUser;

            return store;
        }

        //
        // GET: /Store/Edit/5

        [Authorize]
        public ActionResult Edit(int id)
        {
            var store = MapItDB.Stores.Single(s => s.ID == id);
            return View(store);
        }

        //
        // POST: /Store/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var store = MapItDB.Stores.SingleOrDefault(s => s.ID == id);
            if (store != null)
            {
                var newStoreValues = StoreFromFormCollection(collection);

                store.Name = newStoreValues.Name;
                store.Address = newStoreValues.Address;
                store.City = newStoreValues.City;
                store.State = newStoreValues.State;
                store.Zip = newStoreValues.Zip;
                store.User = this.CurrentUser;

                MapItDB.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Store/Delete/5

        [Authorize]
        public ActionResult Delete(int id)
        {
            var store = MapItDB.Stores.Single(s => s.ID == id);
            return View(store);
        }

        //
        // POST: /Store/Delete/5

        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var store = StoreFromFormCollection(collection);
            store.ID = id;

            MapItDB.Stores.DeleteObject(store);
            return RedirectToAction("Index");
        }

        [NonAction]
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
    }
}
