using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapItPrices.Models.BeerModels.Responses;

namespace MapItPrices.Models.BeerModels
{
    public class MapItResponse
    {
        public Meta Meta { get; set; }
        public Response Response { get; set; }

        public MapItResponse()
        {
            Meta = new Meta();
            Response = new Response();
        }
    }
}