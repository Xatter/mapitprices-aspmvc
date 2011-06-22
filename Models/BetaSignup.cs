using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MapItPrices.Models.Attributes;

namespace MapItPrices.Models
{
    [MetadataType(typeof(BetaSignupMD))]
    public partial class BetaSignup
    {
        public class BetaSignupMD
        {
            [Email]
            [Required(ErrorMessage = "Well at least put *something*")]
            public string Email { get; set; }
        }
    }
}