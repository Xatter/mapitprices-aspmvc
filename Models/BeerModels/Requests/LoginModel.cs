using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapItPrices.Models.BeerModels
{
    public class LoginModel : BaseRequest
    {
        public string SessionToken { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string FoursquareToken { get; set; }
    }
}