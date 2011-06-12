using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapItPrices.Models
{
    public class APICallResult
    {
        public int ErrorCode = 0;
        public string Message = string.Empty;
        public object Data;

        public APICallResult()
        {
        }

        public APICallResult(string message)
        {
            this.Message = message;
        }

        public APICallResult(int code, string message)
        {
            this.ErrorCode = code;
            this.Message = message;
        }

        public APICallResult(int code, object data)
        {
            this.ErrorCode = code;
            this.Data = data;
        }

        public APICallResult(int code, string message, object data)
        {
            this.ErrorCode = code;
            this.Message = message;
            this.Data = data;
        }
    }
}
