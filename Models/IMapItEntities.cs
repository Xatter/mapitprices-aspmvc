using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace MapItPrices.Models
{
    public interface IMapItEntities
    {
        IObjectSet<Item> Items { get; }
        IObjectSet<Store> Stores { get; }
        IObjectSet<StoreItem> StoreItems { get; }

        IObjectSet<User> Users { get; }
        IObjectSet<OpenID> OpenIDs { get; }

        IObjectSet<Role> Roles { get;  }
        IObjectSet<ShoppingList> ShoppingLists { get; }

        IObjectSet<BetaSignup> BetaSignups { get; }
        IObjectSet<BetaInviteCodes> BetaInviteCodes { get; }

        void SaveChanges();
    }
}
