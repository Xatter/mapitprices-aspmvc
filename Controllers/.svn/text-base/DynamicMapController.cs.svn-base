using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapItPrices.Models;
using MvcMaps;
using MapItPrices.ViewModels;

namespace MapItPrices.Controllers
{
    public class DynamicMapController : BaseController
    {
        IMapItEntities mapitDB = new RealDatabaseEntities(new MapItPricesEntities());

        //
        // GET: /DynamicMap/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FindStore(double minLat, double maxLat, double minLng, double maxLng)
        {

            //where double.Parse(d.Attribute("latitude").Value) >= minLat
            //&& double.Parse(d.Attribute("latitude").Value) <= maxLat
            //&& double.Parse(d.Attribute("longitude").Value) >= minLng
            //&& double.Parse(d.Attribute("longitude").Value) <= maxLng

            var stores = from s in mapitDB.Stores
                         select new StoreViewModel()
                         {
                            Name = s.Name,
                            Latitude = s.Latitude,
                            Longitude = s.Longitude
                         };

            var pushpins = from s in stores.ToList()
                           select new Pushpin()
                           {
                               Title = s.Name,
                               Position = new LatLng()
                               {
                                   Latitude = s.Latitude.GetValueOrDefault(0),
                                   Longitude = s.Longitude.GetValueOrDefault(0)
                               }
                           };

            return new MapDataResult()
            {
                Pushpins = pushpins
            };
        }
    }
}
