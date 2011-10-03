using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapItPrices.Models.BeerModels
{
    public class BeerUser
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public BeerUser(User user)
        {
            if (user != null)
            {
                ID = user.ID;
                Email = user.Email;
                Username = user.Username;
            }
        }
    }
}