using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class Product
    {
        public Product()
        {
            Inventories = new HashSet<Inventory>();
            Transactions = new HashSet<Transaction>();
        }

        public string Isbn { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
