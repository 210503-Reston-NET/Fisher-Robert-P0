using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class Order
    {
        public Order()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int OrderNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public int CustId { get; set; }
        public int StoreId { get; set; }
        public decimal Total { get; set; }

        public virtual Customer Cust { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
