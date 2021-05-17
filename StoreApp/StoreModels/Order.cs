using System;
using System.Collections.Generic;

namespace StoreModels
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public int CustomerID { get; set; }
        public int StoreID { get; set; }
        public List<Tuple<Product, int>> Transactions { get; set;}
        public decimal Total { get; set; }
        
        public Order(){}
        public Order(int OrderNumber, int StoreId, int CustomerId, decimal total)
        {
            this.OrderNumber = OrderNumber;
            this.StoreID = StoreId;
            this.CustomerID = CustomerId;
            this.Total = total;
        }
    }
}