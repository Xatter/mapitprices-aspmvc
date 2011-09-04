using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;

namespace MapItPrices.Models.Attributes
{
    public class BeerAuthorization : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            HttpRequestBase request = httpContext.Request;
            string sessionToken = request.Headers["SessionToken"];

            MapItPricesEntities db = new MapItPricesEntities();
            User user = db.Users.SingleOrDefault(u => u.SessionToken == sessionToken);

            if (user == null)
                return false;

            return true;
        }
    }
}