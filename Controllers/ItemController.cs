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
        public ActionResult Edit(Item updatedItem)
        {
            var item = MapItDB.Items.SingleOrDefault(i => i.ID == updatedItem.ID);

            if (item != null)
            {
                item.UPC = updatedItem.UPC;
                item.Size = updatedItem.Size;
                item.Name = updatedItem.Name;
                item.Brand = updatedItem.Brand;
                MapItDB.SaveChanges();
            }

            return RedirectToAction("Index");
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
        public ActionResult Delete(Item updatedItem)
        {
            var item = MapItDB.Items.Single(i => i.ID == updatedItem.ID);
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
            return View(new ReportPriceViewModel());
        }

        [HttpPost]
        [Authorize]
        public ActionResult ReportPrice(ReportPriceViewModel vm)
        {
            if (vm.StoreItem.Item.ID == 0)
            {
                return View(vm);
            }

            StoreItem storeItem = vm.StoreItem;

            if (vm.is_new_store)
            {
                Store store = new Store();
                store.Name = vm.StoreItem.Store.Name;
                store.Address = vm.StoreItem.Store.Address;
                store.City = vm.StoreItem.Store.City;
                store.State = vm.StoreItem.Store.State;
                store.Zip = vm.StoreItem.Store.Zip;
                store.UserID = this.CurrentUser.ID;

                MapItDB.Stores.AddObject(store);
                MapItDB.SaveChanges();

                vm.StoreItem.StoreId = store.ID;
            }
            else
            {
                storeItem = MapItDB.StoreItems.SingleOrDefault(i => i.ItemId == vm.StoreItem.ItemId && i.StoreId == storeItem.StoreId);
                if (storeItem == null)
                {
                    storeItem = new StoreItem();
                    MapItDB.StoreItems.AddObject(storeItem);
                }
            }

            storeItem.ItemId = vm.StoreItem.Item.ID;
            storeItem.StoreId = vm.StoreItem.StoreId;
            storeItem.Price = vm.StoreItem.Price;
            storeItem.UserID = this.CurrentUser.ID;
            storeItem.LastUpdated = DateTime.Now;

            MapItDB.SaveChanges();
            return RedirectToAction("Index", "Map", new { itemid = storeItem.ItemId });
        }

        public string CreateStore(Store storeToCreate)
        {
            var _db = new CommonDBActions(this.MapItDB, this.CurrentUser);
            var id = _db.CreateStore(storeToCreate);
            return id.ToString();
        }
    }
}
