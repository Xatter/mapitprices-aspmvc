using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapItPrices.Models;

namespace MapItPrices.Controllers
{
    public class ShoppingListController : BaseController
    {
        //
        // GET: /ShoppingList/
        [Authorize]
        public ActionResult Index()
        {
            var lists = MapItDB.ShoppingLists.Where(s => s.UserID == this.CurrentUser.ID);
            return View(lists);
        }

        //
        // GET: /ShoppingList/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ShoppingList/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ShoppingList/Create

        [HttpPost]
        public ActionResult Create(ShoppingList shoppingList)
        {
            MapItDB.ShoppingLists.AddObject(shoppingList);
            return RedirectToAction("Index");
        }
        
        //
        // GET: /ShoppingList/Edit/5
 
        public ActionResult Edit(int id)
        {
            var list = MapItDB.ShoppingLists.SingleOrDefault(s => s.Id == id);
            return View(list);
        }

        //
        // POST: /ShoppingList/Edit/5

        [HttpPost]
        public ActionResult Edit(ShoppingList shoppingList)
        {
            return RedirectToAction("Index");
        }

        //
        // GET: /ShoppingList/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ShoppingList/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
