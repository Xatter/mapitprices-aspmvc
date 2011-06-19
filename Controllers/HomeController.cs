using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapItPrices.Models;
using MapItPrices.ViewModels;

namespace MapItPrices.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return RedirectToAction("Index");
            }
            
            var alreadyExists = MapItDB.BetaSignups.SingleOrDefault(s => s.Email.ToUpper() == email.ToUpper());

            if (alreadyExists == null)
            {
                BetaSignup signup = new BetaSignup();
                signup.Email = email;
                MapItDB.BetaSignups.AddObject(signup);
                MapItDB.SaveChanges();
            }

            return RedirectToAction("SayThanks");
        }

        public ActionResult SayThanks()
        {
            return View();
        }

        [Authorize]
        public ActionResult WhoSignedUp()
        {
            return View(MapItDB.BetaSignups.OrderBy(b => b.Id).AsEnumerable());
        }
    }
}
