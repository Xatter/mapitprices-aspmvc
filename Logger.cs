using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapItPrices
{
    interface ILogger
    {
        void Debug(string tolog);
        void Error(string tolog);
    }

    public class Logger
    {
        public Logger()
        {
        }
    }
}