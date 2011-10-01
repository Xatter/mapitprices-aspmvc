using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapItPrices.ViewModels;

namespace MapItPrices.Models.BeerModels.Responses
{
    public class Response
    {
        public BeerUser user { get; set; }
        public BeerItem item { get; set; }
        public BeerStoreResult store { get; set; }
    }
}
