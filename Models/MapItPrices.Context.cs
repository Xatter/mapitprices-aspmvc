//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace MapItPrices.Models
{
    public partial class MapItPricesEntities : ObjectContext
    {
        public const string ConnectionString = "name=MapItPricesEntities";
        public const string ContainerName = "MapItPricesEntities";
    
        #region Constructors
    
        public MapItPricesEntities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public MapItPricesEntities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public MapItPricesEntities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<Item> Items
        {
            get { return _items  ?? (_items = CreateObjectSet<Item>("Items")); }
        }
        private ObjectSet<Item> _items;
    
        public ObjectSet<StoreItem> StoreItems
        {
            get { return _storeItems  ?? (_storeItems = CreateObjectSet<StoreItem>("StoreItems")); }
        }
        private ObjectSet<StoreItem> _storeItems;
    
        public ObjectSet<Store> Stores
        {
            get { return _stores  ?? (_stores = CreateObjectSet<Store>("Stores")); }
        }
        private ObjectSet<Store> _stores;
    
        public ObjectSet<OpenID> OpenIDs
        {
            get { return _openIDs  ?? (_openIDs = CreateObjectSet<OpenID>("OpenIDs")); }
        }
        private ObjectSet<OpenID> _openIDs;
    
        public ObjectSet<User> Users
        {
            get { return _users  ?? (_users = CreateObjectSet<User>("Users")); }
        }
        private ObjectSet<User> _users;
    
        public ObjectSet<Badge> Badges
        {
            get { return _badges  ?? (_badges = CreateObjectSet<Badge>("Badges")); }
        }
        private ObjectSet<Badge> _badges;
    
        public ObjectSet<BetaInviteCodes> BetaInviteCodes
        {
            get { return _betaInviteCodes  ?? (_betaInviteCodes = CreateObjectSet<BetaInviteCodes>("BetaInviteCodes")); }
        }
        private ObjectSet<BetaInviteCodes> _betaInviteCodes;
    
        public ObjectSet<Role> Roles
        {
            get { return _roles  ?? (_roles = CreateObjectSet<Role>("Roles")); }
        }
        private ObjectSet<Role> _roles;
    
        public ObjectSet<ShoppingList> ShoppingLists
        {
            get { return _shoppingLists  ?? (_shoppingLists = CreateObjectSet<ShoppingList>("ShoppingLists")); }
        }
        private ObjectSet<ShoppingList> _shoppingLists;
    
        public ObjectSet<BetaSignup> BetaSignups
        {
            get { return _betaSignups  ?? (_betaSignups = CreateObjectSet<BetaSignup>("BetaSignups")); }
        }
        private ObjectSet<BetaSignup> _betaSignups;

        #endregion
    }
}