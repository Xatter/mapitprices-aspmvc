using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapItPrices.Models;

namespace MapItPrices.Controllers
{
    public abstract class BaseController : Controller
    {
        public IMapItEntities MapItDB { get; private set; }
        User _injectedUser;

        public BaseController()
        {
            MapItDB = new RealDatabaseEntities(new MapItPricesEntities());
        }

        public BaseController(IMapItEntities entities)
        {
            MapItDB = entities;
        }

        public BaseController(User injectedUser) : this()
        {
            _injectedUser = injectedUser;
        }

        public BaseController(IMapItEntities entities, User injectedUser)
        {
            _injectedUser = injectedUser;
            MapItDB = entities;
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewData["IsAuthenticated"] = Request.IsAuthenticated;
            ViewData["IsAdministrator"] = true;

            if (Request.IsAuthenticated)
            {
                var openid = MapItDB.OpenIDs.SingleOrDefault(o => o.ClaimedIdentifier == User.Identity.Name);
                if (openid != null)
                {
                    ViewData["RealName"] = openid.User.RealName;
                }
                else
                {
                    RedirectToAction("Create", "Users", new { identifier = User.Identity.Name });
                }
            }
        }

        public User CurrentUser
        {
            get
            {
                if (_injectedUser == null)
                {
                    var openid = this.MapItDB.OpenIDs.SingleOrDefault(o => o.ClaimedIdentifier == User.Identity.Name);
                    if (openid != null)
                    {
                        return openid.User;
                    }

                    return null;

                }
                else
                {
                    return _injectedUser;
                }
            }

            private set
            {
                _injectedUser = value;
            }
        }
    }
}