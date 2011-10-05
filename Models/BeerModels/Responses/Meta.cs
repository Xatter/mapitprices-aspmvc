using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapItPrices.Models.BeerModels.Responses
{
    public class Meta
    {
        //http://en.wikipedia.org/wiki/List_of_HTTP_status_codes

        public static readonly string OK = "200";
        public static readonly string UNAUTHORIZED = "401";
        public static readonly string CREATED = "201";
        public static readonly string ACCEPTED = "202";

        public static readonly string NOCONTENT = "204";
        public static readonly string RESETCONTENT = "205";
        public static readonly string BADREQUEST = "400";
        public static readonly string ERROR = "500";

        public string Code { get; set; }
        public string ErrorMessage { get; set; }

        public Meta()
        {
            Code = OK;
        }
    }
}
