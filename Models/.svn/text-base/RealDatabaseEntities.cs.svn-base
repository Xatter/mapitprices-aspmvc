using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;

namespace MapItPrices.Models
{
    public class RealDatabaseEntities : IMapItEntities
    {
        readonly MapItPricesEntities _context;

        public IObjectSet<Item> Items
        {
            get { return _context.Items; }
        }

        public IObjectSet<Store> Stores
        {
            get { return _context.Stores; }
        }

        public IObjectSet<StoreItem> StoreItems
        {
            get { return _context.StoreItems; }
        }

        public IObjectSet<User> Users
        {
            get { return _context.Users; }
        }

        public IObjectSet<OpenID> OpenIDs
        {
            get { return _context.OpenIDs; }
        }

        public IObjectSet<Role> Roles
        {
            get { return _context.Roles; }
        }

        public IObjectSet<ShoppingList> ShoppingLists
        {
            get { return _context.ShoppingLists; }
        }

        public RealDatabaseEntities(MapItPricesEntities context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}