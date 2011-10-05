using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapItPrices.Models.BeerModels.Responses
{
    public class LoginResponse : BeerUser
    {
        public LoginResponse(User user) : base(user)
        {
            if (user != null)
            {
                SessionToken = user.SessionToken ?? "";
            }
        }

        public string SessionToken { get; set; }
    }
}