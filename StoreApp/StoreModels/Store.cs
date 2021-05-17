using System.Collections.Generic;
using System;

namespace StoreModels
{
    public class Store
    {
        public string Address { get; set; }
        public string StoreName { get; set; }
        public List<Tuple<Product, int>> Inventory { get; set; }
        public int storeID { get; set; }

        public Store (string StoreName, string Address)
        {
            this.Address = Address;
            this.StoreName = StoreName;
            this.Inventory = new List<Tuple<Product, int>>();
            this.storeID = -1;
        }
        public Store (string StoreName, string Address, List<Tuple<Product, int>> inventory) : this(StoreName, Address)
        {
            this.Inventory = inventory;
        }
        public Store (string StoreName, string Address, List<Tuple<Product, int>> inventory, int storeID) : this(StoreName, Address, inventory)
        {
            this.storeID = storeID;
        }

    }
}