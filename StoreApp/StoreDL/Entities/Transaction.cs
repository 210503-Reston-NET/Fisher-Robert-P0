using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class Transaction
    {
        public string Isbn { get; set; }
        public int OrderNumber { get; set; }
        public int? Quantity { get; set; }

        public virtual Product IsbnNavigation { get; set; }
        public virtual Order OrderNumberNavigation { get; set; }
    }
}
