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
                this.MapItDB.Items.Add(item);
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
            MapItDB.Items.Remove(item);

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

            storeItem = MapItDB.StoreItems.SingleOrDefault(i => i.ItemId == vm.StoreItem.ItemId && i.StoreId == storeItem.StoreId);
            if (storeItem == null)
            {
                storeItem = new StoreItem();
                MapItDB.StoreItems.Add(storeItem);
            }

            storeItem.ItemId = vm.StoreItem.Item.ID;
            storeItem.StoreId = vm.StoreItem.StoreId;
            storeItem.Price = vm.StoreItem.Price;
            storeItem.UserID = this.CurrentUser.ID;
            storeItem.LastUpdated = DateTime.Now;

            MapItDB.SaveChanges();
            return RedirectToAction("Index", "Map", new { itemid = storeItem.ItemId });
        }
    }
}
