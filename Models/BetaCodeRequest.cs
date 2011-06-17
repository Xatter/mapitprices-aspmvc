using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapItPrices.Models
{
    public class BetaCodeRequest
    {
        public string ClaimedIdentifier { get; set; }
        public string ErrorMessage { get; set; }
        public string BetaCode { get; set; }
    }
}