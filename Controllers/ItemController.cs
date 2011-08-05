using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapItPrices.Models;
using MapItPrices.ViewModels;

namespace MapItPrices.Controllers
{
    public class ItemController : BaseController
    {

        public ItemController()
            : base()
        {
        }

        public ItemController(IMapItEntities entities)
            : base(entities)
        {
        }

        //
        // GET: /ItemManager/

        public ActionResult Index()
        {
            var items = this.MapItDB.Items.ToList();
            return View(items);
        }

        //
        // GET: /ItemManager/Details/5

        public ActionResult Details(int id)
        {
            var item = this.MapItDB.Items.Single(i => i.ID == id);
            return View(item);
        }

        //
        // GET: /ItemManager/Create

        [Authorize]
        public ActionResult Create()
        {
            var item = new Item();
            item.User = this.CurrentUser;

            return View(item);
        }

        //
        // POST: /ItemManager/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Item item)
        {
            try
            {
                item.User = this.CurrentUser;
                this.MapItDB.Items.AddObject(item);
                this.MapItDB.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ItemManager/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var item = this.MapItDB.Items.Single(i => i.ID == id);
            item.User = this.CurrentUser;

            return View(item);
        }

        //
        // POST: /ItemManager/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var item = MapItDB.Items.Single(i => i.ID == id);
            var newItemValues = ItemFromFormCollection(collection);

            item.UPC = newItemValues.UPC;
            item.Size = newItemValues.Size;
            item.Name = newItemValues.Name;
            item.Brand = newItemValues.Brand;

            MapItDB.SaveChanges();

            return RedirectToAction("Index");
        }

        private Item ItemFromFormCollection(FormCollection collection)
        {
            var item = new Item();
            item.Name = collection["Name"];
            item.UPC = collection["UPC"];
            item.Size = collection["Size"];
            item.Brand = collection["Brand"];
            item.User = this.CurrentUser;

            return item;
        }

        //
        // GET: /ItemManager/Delete/5
        [MapItAuthorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            var item = this.MapItDB.Items.Single(i => i.ID == id);
            return View(item);
        }

        //
        // POST: /ItemManager/Delete/5

        [HttpPost]
        [MapItAuthorize(Roles = "Administrator")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var item = ItemFromFormCollection(collection);
            item.ID = id;
            MapItDB.Items.DeleteObject(item);

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult UpdatePrice(int id)
        {
            var item = MapItDB.Items.SingleOrDefault(i => i.ID == id);
            ItemPriceUpdateViewModel vm = new ItemPriceUpdateViewModel();
            vm.Stores = MapItDB.Stores.AsEnumerable();
            vm.StoreItem = new StoreItem();
            vm.StoreItem.Item = item;
            vm.StoreItem.ItemId = item.ID;
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdatePrice(StoreItem storeItem)
        {
            var item = MapItDB.StoreItems.SingleOrDefault(si => si.StoreId == storeItem.StoreId && si.ItemId == storeItem.ItemId);

            if (item == null)
            {
                storeItem.LastUpdated = DateTime.Now;
                storeItem.UserID = this.CurrentUser.ID;
                MapItDB.StoreItems.AddObject(storeItem);
            }
            else
            {
                item.UserID = this.CurrentUser.ID;
                item.LastUpdated = DateTime.Now;
                item.Price = storeItem.Price;
            }

            MapItDB.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ReportPrice()
        {
            return View(new StoreItem());
        }

        [HttpPost]
        [Authorize]
        public ActionResult ReportPrice(FormCollection collection)
        {
            if(string.IsNullOrEmpty(collection["Store.ID"]) ||
                string.IsNullOrEmpty(collection["Item.ID"]))
            {
                //ViewData.ModelMetadata
                return View(new StoreItem());
            }

            int storeId;
                if(!int.TryParse(collection["Store.ID"], out storeId))
                {
                    return View(new StoreItem());
                }

            int itemId;
            if(!int.TryParse(collection["Item.ID"], out itemId))
            {
                    return View(new StoreItem());
            }

            decimal price;
            if (!decimal.TryParse(collection["Price"], out price))
            {
                    return View(new StoreItem());
            }

            StoreItem storeItem = MapItDB.StoreItems.SingleOrDefault(i => i.ItemId == itemId && i.StoreId == storeId);

            if (storeItem == null)
            {
                storeItem = new StoreItem();
                MapItDB.StoreItems.AddObject(storeItem);
            }

            storeItem.ItemId = itemId;
            storeItem.StoreId = storeId;
            storeItem.Price = price;
            storeItem.UserID = this.CurrentUser.ID;
            storeItem.LastUpdated = DateTime.Now;

            MapItDB.SaveChanges();
            return RedirectToAction("Index", "Map", new { itemid = storeItem.ItemId });
        }
    }
}
