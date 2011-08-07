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
        public ActionResult Create(Store newStore)
        {
            var _db = new CommonDBActions(this.MapItDB, this.CurrentUser);
            _db.CreateStore(newStore);

            return RedirectToAction("Index", "Store");
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
        public ActionResult Edit(Store updatedStore)
        {
            var _db = new CommonDBActions(this.MapItDB, this.CurrentUser);
            _db.EditStore(updatedStore);
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
        public ActionResult Delete(Store storeToDelete)
        {
            MapItDB.Stores.DeleteObject(storeToDelete);
            MapItDB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
