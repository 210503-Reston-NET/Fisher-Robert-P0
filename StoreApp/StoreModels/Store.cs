using System.Collections.Generic;

namespace StoreModels
{
    public class Store
    {
        public string Address { get; set; }
        public string StoreName { get; set; }
        public List<Product> Inventory { get; set; }

    }
}