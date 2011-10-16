using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapItPrices.Models.BeerModels
{
    public class BeerItem
    {
        public BeerItem()
        {
        }

        public BeerItem(StoreItem newPrice, User user)
        {
            this.ItemId = newPrice.ItemId;
            this.Name = newPrice.Item.Name;
            this.Brand = newPrice.Item.Brand;
            this.Size = newPrice.Item.Size;
            this.UPC = newPrice.Item.UPC;
            this.StoreId = newPrice.StoreId;
            this.Price = newPrice.Price;
            this.Quantity = newPrice.Quantity;
            this.LastUpdated = newPrice.LastUpdated;
            this.User = new BeerUser(user);
        }

        public BeerItem(Item item, User user)
        {
            this.ItemId = item.ID;
            this.Name = item.Name;
            this.Brand = item.Brand;
            this.Size = item.Size;
            this.UPC = item.UPC;
            this.LastUpdated = item.LastUpdated;
            this.User = new BeerUser(user);
        }

        public int ItemId { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
        public BeerUser User { get; set; }
        public string Size { get; set; }
        public string UPC { get; set; }
    }
}