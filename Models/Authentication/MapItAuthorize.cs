using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

namespace MapItPrices.Models
{
    public class MapItAuthorize : AuthorizeAttribute
    {
        MapItPricesEntities mapitDB;

        public MapItAuthorize()
        {
            mapitDB = new MapItPricesEntities();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            IPrincipal user = httpContext.User;
            
            if (!user.Identity.IsAuthenticated)
                return false;

            var openid = mapitDB.OpenIDs.FirstOrDefault(o => o.ClaimedIdentifier == user.Identity.Name);
            
            if (openid == null)
            {
                return false;
            }
            else
            {
                var usersSplit = this.Users.Split(',');
                
                foreach (var userAttribute in usersSplit)
                {
                    if (userAttribute == openid.User.Email)
                        return true;
                }

                var rolesSplit = this.Roles.Split(',');
                var roles = openid.User.Roles.Select(r => r.Name);

                foreach (var role in rolesSplit)
                {
                    if (roles.Contains(role))
                        return true;
                }
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Users/Login");
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}