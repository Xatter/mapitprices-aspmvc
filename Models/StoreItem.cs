//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MapItPrices.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreItem
    {
        public int ItemId { get; set; }
        public int StoreId { get; set; }
        public decimal Price { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public int UserID { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<int> Quantity { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual Store Store { get; set; }
        public virtual User User { internal get; set; }
    }
}
