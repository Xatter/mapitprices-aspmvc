using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.Messaging;
using System.Web.Security;
using MapItPrices.Models;

namespace MapItPrices.Controllers
{
    public class UsersController : BaseController
    {
        private static OpenIdRelyingParty OPENID = new OpenIdRelyingParty();

        MembershipProvider provider;
        RoleProvider roleProvider;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (provider == null) { provider = new MapItMembershipProvider(); }
            if (roleProvider == null) { roleProvider = new MapItRoleProvider(); }

            base.Initialize(requestContext);
        }
        public ActionResult Login()
        {
#if DEBUG
            FormsAuthentication.SetAuthCookie("TEST_USER", false);
            provider.ValidateUser("TEST_USER",string.Empty);

            return RedirectToAction("Index", "Home");
#else 
            return View();
#endif
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        public ActionResult Create(string identifier)
        {
            var openid = new OpenID();
            openid.ClaimedIdentifier = identifier;
            openid.User = new User();

            return View(openid);
        }

        [HttpPost]
        public ActionResult Create(OpenID openid)
        {
            try
            {
                MapItDB.OpenIDs.AddObject(openid);
                MapItDB.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Authenticate()
        {
            var response = OPENID.GetResponse();
            var statusMessage = "";
            if (response == null)
            {
                Identifier id;

                //make sure your users openid_identifier is valid
                if (Identifier.TryParse(Request.Form["openid_identifier"], out id))
                {
                    try
                    {
                        //request openid_identifier
                        return OPENID.CreateRequest(Request.Form["openid_identifier"]).RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException ex)
                    {
                        statusMessage = ex.Message;
                        return View("Login", statusMessage);
                    }
                }
                else
                {
                    statusMessage = "Invalid identifier";
                    return View("Login", statusMessage);
                }
            }
            else
            {
                //check the response status
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        Session["FriendlyIdentifier"] = response.FriendlyIdentifierForDisplay;
                        var identifyer = response.ClaimedIdentifier.ToString();

                        var existingUser = MapItDB.OpenIDs.SingleOrDefault(o => o.ClaimedIdentifier == identifyer);

                        if (existingUser == null)
                        {
                            // Display the 'New Users' dialog
                            return RedirectToAction("Create", new { identifier = response.ClaimedIdentifier.ToString() });
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, false);
                            provider.ValidateUser(response.ClaimedIdentifier, null);
                        }
                        return RedirectToAction("Index", "Home");

                    case AuthenticationStatus.Canceled:
                        statusMessage = "Canceled at provider";
                        return View("Login", statusMessage);
                    case AuthenticationStatus.Failed:
                        statusMessage = response.Exception.Message;
                        return View("Login", statusMessage);
                    default:
                        return new EmptyResult();
                }
            }
        }
    }
}
