using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MapItPrices.Models.Attributes
{
    public class UPCFieldAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(name);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            string upc = value as string;

            if (upc.Length > 12 || upc.Length < 8)
                return false;

            foreach (var character in upc)
            {
                if (!char.IsNumber(character))
                {
                    return false;
                }
            }

            return upc.Length == 12 || upc.Length == 8;
        }
    }
}