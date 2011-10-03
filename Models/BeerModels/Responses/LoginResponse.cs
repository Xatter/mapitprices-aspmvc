using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapItPrices.Models.BeerModels.Responses
{
    public class LoginResponse
    {
        public LoginResponse(User user)
        {
            if (user != null)
            {
                ID = user.ID;
                Email = user.Email;
                Username = user.Username;
                SessionToken = user.SessionToken ?? "";
            }
        }

        public int ID { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string SessionToken { get; set; }
    }
}