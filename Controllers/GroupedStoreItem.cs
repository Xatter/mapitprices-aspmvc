using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapItPrices.Controllers
{
    public class GroupedStoreItem
    {
        public Models.Store Store { get; set; }

        public IEnumerable<Models.StoreItem> StoreItems { get; set; }
    }
}
