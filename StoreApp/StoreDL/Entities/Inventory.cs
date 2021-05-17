using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class Inventory
    {
        public int StoreId { get; set; }
        public string Isbn { get; set; }
        public int Quantity { get; set; }

        public virtual Product IsbnNavigation { get; set; }
        public virtual Store Store { get; set; }
    }
}
