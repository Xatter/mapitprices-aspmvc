using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MapItPrices.Models
{
    [MetadataType(typeof(StoreMD))]
    public partial class Store
    {
        public class StoreMD
        {
            [ScaffoldColumn(false)]
            public int ID { get; set; }

            [Required(ErrorMessage="*Store Name is Required")]
            public string Name { get; set; }
            
            [Required(ErrorMessage="*Street Address is Required")]
            public string Address { get; set; }

            [Required(ErrorMessage="*City is Required")]
            [RegularExpression("^[A-Za-z' ']+$", ErrorMessage="Letters only please.")]
            public string City { get; set; }

            [Required(ErrorMessage="*State is Required")]
            [StringLength(2,ErrorMessage="Please use a state abbreviation, NY, ME, etc.")]
            [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Letters only please.")]
            public string State { get; set; }

            [Required(ErrorMessage="*Zip Code Required")]
            [StringLength(5, ErrorMessage="5 digit Zip Please")]
            [RegularExpression("[0-9]*", ErrorMessage="Numbers only please.")]
            public string Zip { get; set; }
        }
    }
}