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
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;

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
                openid.User.Created = DateTime.Now;
                openid.Created = DateTime.Now;
                MapItDB.Users.Add(openid.User);
                MapItDB.SaveChanges();

                MapItDB.OpenIDs.Add(openid);
                MapItDB.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch(Exception e)
            {
                return View();
            }
        }

        public ActionResult RequestBetaCode(string claimedIdentifier)
        {
            BetaCodeRequest request = new BetaCodeRequest();
            request.ClaimedIdentifier = claimedIdentifier;
            return View(request);
        }

        [HttpPost]
        public ActionResult RequestBetaCode(BetaCodeRequest request)
        {
            var inviteCode = MapItDB.BetaInviteCodes.FirstOrDefault(c => c.InviteCode.ToUpper() == request.BetaCode.ToUpper());
            if (inviteCode == null)
            {
                request.ErrorMessage = "Invalid Beta Code";
                return View("RequestBetaCode", request);
            }
            else
            {
                inviteCode.IsUsed = true;
                MapItDB.SaveChanges();

                return RedirectToAction("Create", new { identifier = request.ClaimedIdentifier });
            }

        }

        public ActionResult Login(string returnUrl)
        {
//#if DEBUG
//            FormsAuthentication.SetAuthCookie("TEST_USER", false);
//            provider.ValidateUser("TEST_USER", string.Empty);

//            return RedirectToAction("Index", "Home");
//#else
            ViewData["returnUrl"] = Url.Encode(returnUrl);

            return View();
//#endif
        }

        [HttpPost]
        public ActionResult Authenticate(FormCollection collection)
        {
            string returnUrl = collection["returnUrl"];
            var response = OPENID.GetResponse();
            //string loginIdentifier = collection["loginIdentifier"];

            //var openid = new OpenIdRelyingParty();
            //IAuthenticationRequest request = openid.CreateRequest(Identifier.Parse(loginIdentifier));
            var statusMessage = "";
            Identifier id;

            //make sure your users openid_identifier is valid
            if (Identifier.TryParse(Request.Form["openid_identifier"], out id))
            {
                try
                {
                    //request openid_identifier
                    var request = OPENID.CreateRequest(Request.Form["openid_identifier"]);

                    if (!string.IsNullOrEmpty(returnUrl))
                        request.AddCallbackArguments("returnUrl", returnUrl);

                    request.AddExtension(new ClaimsRequest
                        {
                            BirthDate = DemandLevel.NoRequest,
                            Email = DemandLevel.NoRequest,
                            FullName = DemandLevel.NoRequest
                        });

                    return request.RedirectingResponse.AsActionResult();
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

        public ActionResult Authenticate()
        {
            var response = OPENID.GetResponse();

            //check the response status
            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:
                    Session["FriendlyIdentifier"] = response.FriendlyIdentifierForDisplay;
                    var identifyer = response.ClaimedIdentifier.ToString();

                    var existingUser = MapItDB.OpenIDs.FirstOrDefault(o => o.ClaimedIdentifier == identifyer);

                    if (existingUser == null)
                    {
                        // Display the 'New Users' dialog
                        return RedirectToAction("RequestBetaCode", new { claimedIdentifier = response.ClaimedIdentifier.ToString() });
                    }
                    else
                    {
                        if (provider.ValidateUser(response.ClaimedIdentifier, null))
                        {
                            FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, true);
                        }
                    }

                    if (Request.Params["ReturnUrl"] != null)
                    {
                        FormsAuthentication.RedirectFromLoginPage(response.ClaimedIdentifier, true);
                    }
                    else
                    {
                        Response.Redirect("/");
                    }
                    break;
                case AuthenticationStatus.Canceled:
                    ModelState.AddModelError("loginIdentifier", "Canceled at provider");
                    break;
                case AuthenticationStatus.Failed:
                    ModelState.AddModelError("loginIdentifier", response.Exception.Message);
                    break;
            }

            return View();
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(MapItDB.Users);
        }
    }
}
