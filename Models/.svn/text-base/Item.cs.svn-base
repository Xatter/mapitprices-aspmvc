using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MapItPrices.Models.Attributes;

namespace MapItPrices.Models
{
    [MetadataType(typeof(ItemMD))]
    public partial class Item
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public class ItemMD
        {
            [ScaffoldColumn(false)]
            public int ID { get; set; }

            [Required(ErrorMessage="*Item Name is Required")]
            public string Name { get; set; }

            [UPCField]
            [RegularExpression("[0-9]*", ErrorMessage = "Numbers only please.")]
            public string UPC { get; set; }
        }
    }
}