using System.Collections.Generic;
using StoreModels;
using StoreDL;

namespace StoreBL
{
    public class StoreBussinessLayer : StoreBLInterface
    {
        private DAO _repo;
        private RepoDB _repoDB;
        public StoreBussinessLayer(RepoDB repo)
        {
            _repoDB = repo;
        }
        public List<Inventory> GetInventory(int storeID)
        {
            return _repoDB.getInventory(storeID);
        }
        public bool AddProduct(Product product)
        {
            return _repoDB.AddProduct(product);
        }
        public bool AddOrder(Order order)
        {
            _repoDB.AddOrder(order);
            return true;

        }

        public Product GetProduct(string ISBN)
        {
            return _repoDB.GetProduct(ISBN);
        }

        public List<Product> GetAllProducts()
        {
            return _repoDB.GetProducts();
        }

        public User GetUser(string UserName, string Password)
        {
            return _repoDB.GetUser(UserName);
        }

        public List<User> GetAllUsers()
        {
            return _repoDB.GetAllUsers();
        }

        public bool AddUser(User user)
        {
            return _repoDB.AddUser(user);
        }

        public Store GetStore(Store TargetStore)
        {
            return _repoDB.GetStore(TargetStore);
        }

        public Order GetOrder(Order order)
        {
            return _repoDB.GetOrder(order);
        }

        public bool AddTransaction(Transaction transact)
        {
            return _repoDB.AddTransaction(transact);
        }

        public bool RemoveProduct(Product product)
        {
            return _repoDB.RemoveProduct(product);
        }

        public bool UpdateProduct(Product EditedProduct)
        {
            return _repoDB.UpdateProduct(EditedProduct);
        }
    }
}